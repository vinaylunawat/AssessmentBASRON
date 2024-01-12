using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASRON.ServerlessTest.Models
{
    public class UpdateTransactionResponse
    {
        public UpdateBTransaction updateBTransaction { get; set; }
    }

    public class UpdateBTransaction
    {
        public string customerId { get; set; }
        public DateTime transactionDate { get; set; }
        public string referenceNumber { get; set; }
        public double amount { get; set; }
        public string transactionType { get; set; }
        public string productType { get; set; }
        public string currency { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public bool isActive { get; set; }
    }
     
}
