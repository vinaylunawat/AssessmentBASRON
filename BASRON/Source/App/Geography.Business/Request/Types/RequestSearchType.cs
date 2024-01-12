using GraphQL.Types;
using System;

namespace BASRON.Business.Request.Types
{
    public class RequestSearchType : InputObjectGraphType
    {
        public RequestSearchType()
        {
            Name = "requestSearchInput";
            Field<IdGraphType>("customerId");
            Field<DateTimeGraphType>("transactionDate");
            Field<IdGraphType>("referenceNumber");
            Field<StringGraphType>("customerName");
            Field<DecimalGraphType>("amount");
            Field<StringGraphType>("status");
            Field<StringGraphType>("remark");
            Field<StringGraphType>("transactionType");
            Field<DateTimeGraphType>("createdDate");
            Field<BooleanGraphType>("isActive");
        }
    } 
}
