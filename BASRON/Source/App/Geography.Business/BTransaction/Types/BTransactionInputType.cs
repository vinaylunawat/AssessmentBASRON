using GraphQL.Types;
using System;

namespace BASRON.Business.BTransaction.Types
{
    public class BTransactionCreateInputType : InputObjectGraphType
    {
        public BTransactionCreateInputType()
        {
            Name = "btransactionCreateInput";
            Field<NonNullGraphType<IdGraphType>>("customerId");
            Field<NonNullGraphType<DateGraphType>>("transactionDate");
            Field<NonNullGraphType<IdGraphType>>("referenceNumber");
            Field<NonNullGraphType<DecimalGraphType>>("amount");           
            Field<NonNullGraphType<StringGraphType>>("transactionType");
            Field<NonNullGraphType<StringGraphType>>("customerName");
            Field<NonNullGraphType<StringGraphType>>("productType");
            Field<NonNullGraphType<StringGraphType>>("currency");
            Field<NonNullGraphType<DateGraphType>>("createdDate");
            Field<NonNullGraphType<DateGraphType>>("updatedDate");
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
            Field<NonNullGraphType<DateGraphType>>("trasctionDate");
            Field<NonNullGraphType<StringGraphType>>("trasctionType");
            Field<NonNullGraphType<IdGraphType>>("referenceNumber");
            Field<NonNullGraphType<StringGraphType>>("trasctionStatus");
            Field<NonNullGraphType<BooleanGraphType>>("isActive");
        }
    }
 
}
