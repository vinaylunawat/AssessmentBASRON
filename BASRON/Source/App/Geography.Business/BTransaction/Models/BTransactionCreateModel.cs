using Amazon.DynamoDBv2.DataModel;
using System;

namespace BASRON.Business.BTransaction.Models
{
    /// <summary>
    /// Defines the <see cref="BTransactionCreateModel" />.
    /// </summary>
    public class BTransactionCreateModel
    {        
        public Guid CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public string TransactionType { get; set; }
        public string ProductType { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate{ get; set; }
        public bool IsActive { get; set; }
    }
}
