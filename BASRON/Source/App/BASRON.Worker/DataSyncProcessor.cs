using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Framework.DataAccess;
using BASRON.DataAccess;
using BASRON.DataAccess.Repository;
using AutoMapper;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BASRON.Worker;

public class DataSyncProcessor
{
    private readonly IBTransactionRepository bTransactionRepository;
    private readonly IRequestRepository requestRepository;
    private readonly IMapper mapper;
    public IConfiguration Configuration { get; private set; }

    public DataSyncProcessor()
    {
        var serviceCollection = new ServiceCollection();
        Configuration = new ConfigurationBuilder()
               .AddJsonFile("appSettings.json", optional: true)
        .Build();

        serviceCollection.ConfigureDbServices();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        bTransactionRepository = serviceProvider.GetService<IBTransactionRepository>();
        requestRepository = serviceProvider.GetService<IRequestRepository>();
        mapper = serviceProvider.GetService<IMapper>();

    }

    public async Task ProcessAsync(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");
        //var model = JsonConvert.DeserializeObject<TModel>(message.Body);

        var transactionDetail = mapper.Map<Entity.Entities.BTransaction>(message.Body);
        if(transactionDetail != null)
        {
            await bTransactionRepository.CreateAsync(transactionDetail, default).ConfigureAwait(false);
            var request = await requestRepository.GetByKey(transactionDetail.ReferenceNumber, default).ConfigureAwait(false);
            if (request is not null)
            {
                request.Status = "Reconciled";
                await requestRepository.UpdateAsync(request, default).ConfigureAwait(false);
            }
        }

        await Task.CompletedTask;
    }
}