using GraphQL.Types;
using System;

namespace BASRON.Business.Request.Types
{
    public class RequestCreateInputType : InputObjectGraphType
    {
        public RequestCreateInputType()
        {
            Name = "requestCreateInput";
            Field<IdGraphType>("customerId");
            Field<NonNullGraphType<DateTimeGraphType>>("transactionDate");
            Field <NonNullGraphType<IdGraphType>>("referenceNumber");
            Field<StringGraphType>("customerName");
            Field<NonNullGraphType<DecimalGraphType>>("amount");
            Field<StringGraphType>("status");
            Field<StringGraphType>("remark");
            Field<StringGraphType>("transactionType");
            Field<DateTimeGraphType>("createdDate");
            Field<BooleanGraphType>("isActive");
        }
    } 
}
