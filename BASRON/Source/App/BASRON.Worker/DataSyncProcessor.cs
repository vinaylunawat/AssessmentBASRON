using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Framework.DataAccess;
using BASRON.DataAccess;
using BASRON.DataAccess.Repository;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BASRON.Worker;

public class DataSyncProcessor
{

    public IConfiguration Configuration { get; private set; }

    private readonly IBTransactionRepository bTransactionRepository;

    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public DataSyncProcessor()
    {
        var serviceCollection = new ServiceCollection();
        Configuration = new ConfigurationBuilder()
               .AddJsonFile("appSettings.json", optional: true)
        .Build();

        serviceCollection.ConfigureDbServices();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        bTransactionRepository = serviceProvider.GetService<IBTransactionRepository>();

    }

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
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


        // TODO: Do interesting work based on the new message
        await Task.CompletedTask;
    }
}