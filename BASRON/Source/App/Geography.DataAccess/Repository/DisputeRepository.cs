namespace BASRON.DataAccess.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Framework.DataAccess.Repository;
    using BASRON.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="DisputeRepository" />.
    /// </summary>
    public class DisputeRepository : GenericRepository<Dispute>, IDisputeRepository
    {
        private readonly IAmazonDynamoDB _client;
        public DisputeRepository(IDynamoDBContext dbContext, IAmazonDynamoDB client) : base(dbContext, client)
        {
            _client = client;
        }                 
    }
}
