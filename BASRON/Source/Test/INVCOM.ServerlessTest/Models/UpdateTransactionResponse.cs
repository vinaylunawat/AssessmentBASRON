using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INVCOM.ServerlessTest.Models
{
    public class UpdateTransactionResponse
    {
        public UpdateBTransaction updateTransaction { get; set; }
    }

    public class UpdateBTransaction
    {
        public string referenceNumber { get; set; }
        public string customerId { get; set; }
        public string transactionDate { get; set; } 
        public double amount { get; set; }
        public string transactionType { get; set; }
        public string productType { get; set; }
        public string currency { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
        public bool isActive { get; set; }


    }
}
