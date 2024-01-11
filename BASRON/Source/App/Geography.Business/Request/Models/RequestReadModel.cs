using System;

namespace BASRON.Business.Request.Models
{
    /// <summary>
    /// Defines the <see cref="RequestReadModel" />.
    /// </summary>
    public class RequestReadModel
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
    }
}
