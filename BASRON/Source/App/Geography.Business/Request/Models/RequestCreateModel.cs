using Amazon.DynamoDBv2.DataModel;
using System;

namespace BASRON.Business.Request.Models
{
    /// <summary>
    /// Defines the <see cref="RequestCreateModel" />.
    /// </summary>
    public class RequestCreateModel
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }

        public string Remark { get; set; }
        public string TransactionType { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

    }
}
