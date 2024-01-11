using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using BASRON.Entity.Entities;
using Framework.DataAccess.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BASRON.DataAccess
{
    public class DisputeTableCreationProvider : DynamoDBClientProviderBase
    {
        private readonly ILogger<DynamoDBClientProviderBase> _logger;
        private readonly IAmazonDynamoDB _client;
        private const string TableName = "Dispute";

        public DisputeTableCreationProvider(ILogger<DynamoDBClientProviderBase> logger, IAmazonDynamoDB amazonDynamoDBClient)
            : base(logger, amazonDynamoDBClient)
        {
            _logger = logger;
            _client = amazonDynamoDBClient;
        }

        public override async Task CreateTable()
        {
            Dispute dispute;
            var request = new CreateTableRequest
            {
                TableName = TableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                    {
                        new AttributeDefinition
                        {
                                AttributeName = nameof(dispute.ReferenceNumber),
                                AttributeType = ScalarAttributeType.S
                        },                       
                    },
                KeySchema = new List<KeySchemaElement>()
                        {
                             new KeySchemaElement
                             {
                                    AttributeName = nameof(dispute.ReferenceNumber),
                                    KeyType =KeyType.HASH
                             },
                             
                        },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            };
            await _client.CreateTableAsync(request);
        }
    }
}
