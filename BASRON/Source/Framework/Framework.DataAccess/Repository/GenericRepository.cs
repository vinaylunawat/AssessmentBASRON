﻿namespace Framework.DataAccess.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using Amazon.DynamoDBv2.Model;
    using Amazon.Runtime;
    using Amazon.S3;
    using Amazon.S3.Model;
    using EnsureThat;
    using Framework.Configuration.Models;
    using Framework.DataAccess.Model;
    using LinqKit;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class GenericRepository<TEntity> : RepositoryBase<TEntity>, IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly IAmazonDynamoDB _client;
        public GenericRepository(IDynamoDBContext context, IAmazonDynamoDB client) : base(context)
        {
            _client = client;
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.ScanAsync<TEntity>(default).GetRemainingAsync(cancellationToken);
        }

        public async Task<TEntity> GetByKey<TKey>(TKey key, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<TEntity>(key, cancellationToken);
        }

        public async Task<bool> GetDetailsByAttributeName(string attributeName, string attributeValue)
        {
            var request = new ScanRequest
            {
                TableName = "Country",
                ExpressionAttributeNames = new Dictionary<string, string>
                {
                  { $"#{attributeName}", attributeName },
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":name", new AttributeValue { S = attributeValue } },
                },
                FilterExpression = $"#{attributeName} = :name",
                ProjectionExpression = $"#{attributeName}, Id",
                Limit = 10
            };

            var response = await _client.ScanAsync(request).ConfigureAwait(false);

            if (response.Count > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<TEntity> GetByKey<TKey, TRangeKey>(TKey key, TRangeKey rangeKey, CancellationToken cancellationToken)
        {
            return await _dynamoDBContext.LoadAsync<TEntity>(key, rangeKey, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetPaginatedScanItemsAsync(Dictionary<string, string> data = default)
        {
            //Dictionary<string, AttributeValue> lastKeyEvaluated = !lastEvaluatedKey.IsNullOrEmpty() ? new Dictionary<string, AttributeValue> { { "Id", new AttributeValue(lastEvaluatedKey) } } : null;

            var request = new ScanRequest
            {
                TableName = typeof(TEntity).Name,
                //ExclusiveStartKey = lastKeyEvaluated,
                //Limit = pageSize,
            };

            //var expressionAttributeName = new Dictionary<string, string>();
            var expressionAttributeValues = new Dictionary<string, AttributeValue>();
            var filterExpression = new StringBuilder();
            foreach (var item in data)
            {
                expressionAttributeValues.Add($":{item.Key.ToLower()}", new AttributeValue(item.Value));
                filterExpression.Append($"{item.Key} = :{item.Key.ToLower()}");
                filterExpression.Append(" AND ");
            }

            var filterExpressionValue = filterExpression.ToString();

            filterExpressionValue = filterExpressionValue.Substring(0, filterExpressionValue.LastIndexOf(" AND "));

            // if (projectionFields != null && projectionFields.Any())
            {
                //request.ProjectionExpression = string.Join(",", projectionFields.Select(a => $"#{a}"));
                var attrNames = new Dictionary<string, string>();
                //projectionFields.ForEach(a => attrNames.Add($"#{a}", a));
                //request.UpdateExpression = updateExpression,
                //request.ExpressionAttributeNames = expressionAttributeName;
                request.ExpressionAttributeValues = expressionAttributeValues;
                request.FilterExpression = filterExpressionValue;
            }

            var response = await _client.ScanAsync(request);

            var items = response.Items.Select(a =>
            {
                var doc = Document.FromAttributeMap(a);
                return _dynamoDBContext.FromDocument<TEntity>(doc);
            }).ToList();


            return items;

            //var hasMorePages = response.LastEvaluatedKey != null;

            //return new PagedResultModel<TEntity>(items, hasMorePages ? response.LastEvaluatedKey["ReferenceNumber"].S : null, hasMorePages);
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
            return entity;
        }

        public async Task DeleteAsync<TKey>(TKey key, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.DeleteAsync<TEntity>(key, cancellationToken);
        }
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dynamoDBContext.SaveAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<string> UploadFileAsync(AmazonS3ConfigurationOptions amazonS3Configuration, string fileName, Stream fileContent)
        {
            EnsureArg.IsNotNull(amazonS3Configuration, nameof(amazonS3Configuration));

            var credentials = new BasicAWSCredentials(amazonS3Configuration.AccessKey, amazonS3Configuration.SecretKey);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }
            string key = string.IsNullOrEmpty(amazonS3Configuration.Prefix) ? fileName : $"{amazonS3Configuration.Prefix?.TrimEnd('/')}/{fileName}";
            using (var amazonS3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.GetBySystemName(amazonS3Configuration.Region)))
            {
                try
                {
                    var response = await amazonS3Client.PutObjectAsync(new PutObjectRequest { BucketName = amazonS3Configuration.BucketName, Key = key, InputStream = fileContent }).ConfigureAwait(false);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var request = new GetPreSignedUrlRequest()
                        {
                            BucketName = amazonS3Configuration.BucketName,
                            Key = key,
                            Expires = DateTime.UtcNow.AddDays(amazonS3Configuration.PreSignedExpiresDays)
                        };
                        return await amazonS3Client.GetPreSignedURLAsync(request);
                    }
                    else
                    {
                        Console.WriteLine($"error occurred with http status code {response.HttpStatusCode} while uploading a file");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
                
            }
        }
    }
}
