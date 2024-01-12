using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BASRON.ServerlessTest.Models
{
    public class ValidQueryResult
    {
        public List<BTransaction> bTransactions { get; set; }
    }
    public class BTransaction
    {
        public string referenceNumber { get; set; }
        public string customerId { get; set; }
        public double amount { get; set; }
        public string transactionType { get; set; }
        public string productType { get; set; }
        public string currency { get; set; }
    }
}
