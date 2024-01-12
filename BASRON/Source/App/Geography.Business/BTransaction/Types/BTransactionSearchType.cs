using GraphQL.Types;
using System;

namespace BASRON.Business.BTransaction.Types
{
    public class BTransactionSearchType : InputObjectGraphType
    {
        public BTransactionSearchType()
        {
            Name = "btransactionSearchInput";
            Field<StringGraphType>("customerId");
            Field<DateTimeGraphType>("transactionDate");
            Field<IdGraphType>("referenceNumber");
            Field<DecimalGraphType>("amount");           
            Field<StringGraphType>("transactionType");
            Field<StringGraphType>("customerName");
            Field<StringGraphType>("productType");
            Field<StringGraphType>("currency");
            Field<DateTimeGraphType>("createdDate");
            Field<BooleanGraphType>("isActive");
        }
    } 
}
