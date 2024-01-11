namespace BASRON.DataAccess.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Framework.DataAccess.Repository;
    using BASRON.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="RequestRepository" />.
    /// </summary>
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        private readonly IAmazonDynamoDB _client;
        public RequestRepository(IDynamoDBContext dbContext, IAmazonDynamoDB client) : base(dbContext, client)
        {
            _client = client;
        }                 
    }
}
