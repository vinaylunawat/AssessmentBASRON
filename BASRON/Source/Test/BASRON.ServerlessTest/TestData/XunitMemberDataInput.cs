using Amazon.Util;
using BASRON.Serverless.Model;

namespace BASRON.ServerlessTest.TestData
{

    public class XunitMemberDataInput
    {
         private static  DateTime transDate = DateTime.Now;
        public static IEnumerable<object[]> GraphQLModelData() =>
        new List<GraphQLModel[]>
          {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query bTransactions { bTransactions { referenceNumber ,customerId,amount,transactionType,productType,currency} }",
                                          Variables=null
                        }
                    }

          };
        public static IEnumerable<object[]> GraphQLModelDataForCreate() =>
        new List<GraphQLModel[]>
         {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query bTransactions { bTransactions { referenceNumber ,customerId,amount,transactionType,productType,currency} }",
                                          Variables=null
                        },
                         new GraphQLModel{ Query="mutation($btransaction: btransactionCreateInput!) { createBTransaction(btransaction: $btransaction) { customerId  transactionDate  referenceNumber amount  transactionType productType currency  createdDate updatedDate isActive} }" ,
                                          Variables=new Dictionary<string, object>
                                          {

                                              {   "btransaction", new Dictionary<string ,object>{
                                                {  "customerId",Guid.NewGuid() },
                                                { "transactionDate",transDate},
                                                {  "referenceNumber",Guid.NewGuid()},
                                                {  "amount",100},
                                                { "transactionType", "transaction type1"},
                                                {  "productType", "product type 1"},
                                                {  "currency", "$"},
                                                {  "createdDate", transDate},
                                                {  "updatedDate",transDate},
                                                {  "isActive", true},
                                                { "customerName", "Test"}} 
                                              }
                                          }

                        }
                    }

         };

        public static IEnumerable<object[]> GraphQLModelDataForDelete() =>
       new List<GraphQLModel[]>
        {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query bTransactions { bTransactions { referenceNumber ,customerId,amount,transactionType,productType,currency} }",
                                          Variables=null
                        },
                         new GraphQLModel{ Query="mutation($btransaction: btransactionCreateInput!) { createBTransaction(btransaction: $btransaction) { customerId  transactionDate  referenceNumber amount  transactionType productType currency  createdDate updatedDate isActive} }" ,
                                          Variables=new Dictionary<string, object>
                                          {

                                              {   "btransaction", new Dictionary<string ,object>{
                                                {  "customerId",Guid.NewGuid() },
                                                { "transactionDate",transDate},
                                                {  "referenceNumber",Guid.NewGuid()},
                                                {  "amount",100},
                                                { "transactionType", "transaction type1"},
                                                {  "productType", "product type 1"},
                                                {  "currency", "$"},
                                                {  "createdDate", transDate},
                                                {  "updatedDate",transDate},
                                                {  "isActive", true},
                                                { "customerName", "Test"}}
                                              }
                                          }

                        },
                        new GraphQLModel{ Query="mutation($btransactionId: ID!) { deleteBTransaction(btransactionId: $btransactionId) }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "btransactionId", "-1" }
                                          }
                        }
                    }

        };

        public static IEnumerable<object[]> GraphQLModelDataForUpdate() =>
       new List<GraphQLModel[]>
        {
                    new GraphQLModel[]
                    {
                        new GraphQLModel{ Query="query bTransactions { bTransactions { referenceNumber ,customerId,amount,transactionType,productType,currency} }",
                                          Variables=null
                        },
                          new GraphQLModel{ Query="mutation($btransaction: btransactionCreateInput!) { createBTransaction(btransaction: $btransaction) { customerId  transactionDate  referenceNumber amount  transactionType productType currency  createdDate updatedDate isActive} }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              {   "btransaction", new Dictionary<string ,object>{
                                                {  "customerId",Guid.NewGuid() },
                                                { "transactionDate",DateTime.Now},
                                                {  "referenceNumber",Guid.NewGuid()},
                                                {  "amount",500},
                                                { "transactionType", "transaction type1"},
                                                {  "productType", "product type 1"},
                                                {  "currency", "$"},
                                                {  "createdDate",transDate},
                                                {  "updatedDate",transDate},
                                                {  "isActive", false},
                                                { "customerName", "Updated Name"}}
                                              }
                                          }

                        },
                          new GraphQLModel{ Query="mutation($btransaction: btransactionUpdateInput!) { updateBTransaction(btransaction: $btransaction) { customerId  transactionDate  referenceNumber amount  transactionType productType currency  createdDate updatedDate isActive} }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              {   "btransaction", new Dictionary<string ,object>{
                                                {  "customerId",Guid.NewGuid() },
                                                { "transactionDate",transDate},
                                                {  "referenceNumber",Guid.NewGuid()},
                                                {  "amount",500},
                                                { "transactionType", "transaction type1"},
                                                {  "productType", "product type 1"},
                                                {  "currency", "$"},
                                                {  "createdDate",transDate},
                                                {  "updatedDate",transDate},
                                                {  "isActive", false},
                                                { "customerName", "Updated Name"}}
                                              }
                                          }

                        },
                        new GraphQLModel{ Query="mutation($btransactionId: ID!) { deleteBTransaction(btransactionId: $btransactionId) }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "btransactionId", "-1" }
                                          }
                        }
                    }

        };
        public static IEnumerable<object[]> GraphQLModelDataForInvalidUpdate() =>
       new List<GraphQLModel[]>
           {
                    new GraphQLModel[]
                    {
                         new GraphQLModel{ Query="mutation($Transaction: TransactionUpdateInput!) { updateTransaction(Transaction: $Transaction) { referenceNumber transactionAmount transactionType } }" ,
                                            Variables=new Dictionary<string, object>
                                          {
                                              {   "btransaction", new Dictionary<string ,object>{
                                                {  "customerId",Guid.NewGuid() },
                                                { "transactionDate",transDate},
                                                {  "referenceNumber",Guid.NewGuid()},
                                                {  "amount",500},
                                                { "transactionType", "transaction type1"},
                                                {  "productType", "product type 1"},
                                                {  "currency", "$"},
                                                {  "createdDate",transDate},
                                                {  "updatedDate",transDate},
                                                {  "isActive", false},
                                                { "customerName", "Updated Name"}}
                                              }
                                          }
                        }
                    }

           };
        public static IEnumerable<object[]> GraphQLModelDeleteInvalidData() =>
        new List<GraphQLModel[]>
           {
                    new GraphQLModel[]
                    {
                          new GraphQLModel{ Query="mutation($btransactionId: ID!) { deleteBTransaction(btransactionId: $btransactionId) }" ,
                                          Variables=new Dictionary<string, object>
                                          {
                                              { "btransactionId", "-1" }
                                          }
                        }
                    }

           };


        public static IEnumerable<object[]> GraphQLModelDataWithIsoAlreadyExist() =>
        new List<GraphQLModel[]>
         {
                    new GraphQLModel[]
                    {
                         new GraphQLModel{ Query="mutation($Transaction: TransactionCreateInput!) { createTransaction(Transaction: $Transaction) { referenceNumber transactionAmount transactionType } }" ,
                                           Variables=new Dictionary<string, object>
                                          {
                                              {   "btransaction", new Dictionary<string ,object>{
                                                {  "customerId",Guid.NewGuid() },
                                                { "transactionDate",transDate},
                                                {  "referenceNumber",Guid.NewGuid()},
                                                {  "amount",500},
                                                { "transactionType", "transaction type1"},
                                                {  "productType", "product type 1"},
                                                {  "currency", "$"},
                                                {  "createdDate",transDate},
                                                {  "updatedDate",transDate},
                                                {  "isActive", false},
                                                { "customerName", "Updated Name"}}
                                              }
                                          }
                        }
                    }

         };
    }
}
