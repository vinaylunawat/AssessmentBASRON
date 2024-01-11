using Amazon.DynamoDBv2.DataModel;
using System;

namespace BASRON.Business.Dispute.Models
{
    /// <summary>
    /// Defines the <see cref="DisputeCreateModel" />.
    /// </summary>
    public class DisputeCreateModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }

        public string Remark { get; set; }
        public string TransactionType { get; set; } 
    }
}
