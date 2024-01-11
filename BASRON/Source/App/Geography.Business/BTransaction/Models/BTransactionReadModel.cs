using System;

namespace BASRON.Business.BTransaction.Models
{
    /// <summary>
    /// Defines the <see cref="BTransactionReadModel" />.
    /// </summary>
    public class BTransactionReadModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string ProductType { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
