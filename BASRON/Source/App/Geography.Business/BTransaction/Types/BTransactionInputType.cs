using GraphQL.Types;
using System;

namespace BASRON.Business.BTransaction.Types
{
    public class BTransactionCreateInputType : InputObjectGraphType
    {
        public BTransactionCreateInputType()
        {
            Name = "btransactionCreateInput";
            Field<NonNullGraphType<StringGraphType>>("customerId");
            Field<NonNullGraphType<DateTimeGraphType>>("transactionDate");
            Field<NonNullGraphType<IdGraphType>>("referenceNumber");
            Field<NonNullGraphType<DecimalGraphType>>("amount");           
            Field<NonNullGraphType<StringGraphType>>("transactionType");
            Field<NonNullGraphType<StringGraphType>>("customerName");
            Field<NonNullGraphType<StringGraphType>>("productType");
            Field<NonNullGraphType<StringGraphType>>("currency");
            Field<NonNullGraphType<DateTimeGraphType>>("createdDate");
            Field<NonNullGraphType<DateTimeGraphType>>("updatedDate");
            Field<NonNullGraphType<BooleanGraphType>>("isActive");
        }
    }

    public class BTransactionUpdateInputType : InputObjectGraphType
    {
        public BTransactionUpdateInputType()
        {
            Name = "btransactionUpdateInput";
            Field<IdGraphType>("Id");
            Field<NonNullGraphType<StringGraphType>>("customerId");
            Field<NonNullGraphType<DecimalGraphType>>("amount");
            Field<NonNullGraphType<DateTimeGraphType>>("trasctionDate");
            Field<NonNullGraphType<StringGraphType>>("trasctionType");
            Field<NonNullGraphType<IdGraphType>>("referenceNumber");
            Field<NonNullGraphType<StringGraphType>>("trasctionStatus");
            Field<NonNullGraphType<BooleanGraphType>>("isActive");
        }
    }
 
}
