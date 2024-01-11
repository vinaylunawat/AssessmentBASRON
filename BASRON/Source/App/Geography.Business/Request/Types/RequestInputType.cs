using GraphQL.Types;
using System;

namespace BASRON.Business.Request.Types
{
    public class RequestCreateInputType : InputObjectGraphType
    {
        public RequestCreateInputType()
        {
            Name = "requestCreateInput";
            Field<NonNullGraphType<IdGraphType>>("customerId");
            Field<NonNullGraphType<DateGraphType>>("transactionDate");
            Field<IdGraphType>("referenceNumber");
            Field<NonNullGraphType<StringGraphType>>("customerName");
            Field<NonNullGraphType<DecimalGraphType>>("amount");
            Field<NonNullGraphType<StringGraphType>>("status");
            Field<NonNullGraphType<StringGraphType>>("remark");
            Field<NonNullGraphType<StringGraphType>>("transactionType");                     
        }
    } 
}
