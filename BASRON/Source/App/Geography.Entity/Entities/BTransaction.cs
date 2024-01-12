namespace BASRON.Entity.Entities
{
    using Amazon.DynamoDBv2.DataModel;
    using Framework.Entity;
    using Framework.Entity.Entities;
    using System;


    [DynamoDBTable("BTransaction")]
    public class BTransaction 
    {
        [DynamoDBHashKey("ReferenceNumber")]
        public Guid ReferenceNumber { get; set; }

        [DynamoDBProperty("CustomerName")]
        public string CustomerName { get; set; }

        [DynamoDBProperty("CustomerId")]
        public string CustomerId { get; set; }

        [DynamoDBProperty("TransactionDate")]
        public DateTime TransactionDate { get; set; }
        
        [DynamoDBProperty("Amount")]
        public decimal Amount { get; set; }

        [DynamoDBProperty("TransactionType")]
        public string TransactionType { get; set; }

        [DynamoDBProperty("ProductType")]
        public string ProductType { get; set; }

        [DynamoDBProperty("Currency")]
        public string Currency { get; set; }

        [DynamoDBProperty("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [DynamoDBProperty("UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        [DynamoDBProperty("IsActive")]
        public bool IsActive { get; set; }

    }
}

 
