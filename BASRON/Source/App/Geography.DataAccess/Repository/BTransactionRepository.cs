namespace BASRON.DataAccess.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Framework.DataAccess.Repository;
    using BASRON.Entity.Entities;

    /// <summary>
    /// Defines the <see cref="BTransactionRepository" />.
    /// </summary>
    public class BTransactionRepository : GenericRepository<BTransaction>, IBTransactionRepository
    {
        private readonly IAmazonDynamoDB _client;
        public BTransactionRepository(IDynamoDBContext dbContext, IAmazonDynamoDB client) : base(dbContext, client)
        {
            _client = client;
        }                 
    }
}
