namespace BASRON.Entity.Entities
{
    using Amazon.DynamoDBv2.DataModel;
    using Framework.Entity;
    using Framework.Entity.Entities;
    using System;


    [DynamoDBTable("Dispute")]
    public class Dispute
    {
        [DynamoDBHashKey("ReferenceNumber")]
        public Guid ReferenceNumber { get; set; }

        [DynamoDBProperty("CustomerName")]
        public string CustomerName { get; set; }

        [DynamoDBProperty("CustomerId")]
        public Guid CustomerId { get; set; }

        [DynamoDBProperty("TransactionDate")]
        public DateTime TransactionDate { get; set; }

        [DynamoDBProperty("Amount")]
        public decimal Amount { get; set; }

        [DynamoDBProperty("Status")]
        public string Status { get; set; }

        [DynamoDBProperty("Remark")]
        public string Remark { get; set; }

        [DynamoDBProperty("TransactionType")]
        public string TransactionType { get; set; }
    }
}

 
