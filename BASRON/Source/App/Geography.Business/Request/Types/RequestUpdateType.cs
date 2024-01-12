using GraphQL.Types;
using System;

namespace BASRON.Business.Request.Types
{
    public class RequestUpdateInputType : InputObjectGraphType
    {
        public RequestUpdateInputType()
        {
            Name = "requestUpdateInput";
            Field<IdGraphType>("referenceNumber");
            Field<NonNullGraphType<StringGraphType>>("status");
            Field<StringGraphType>("remark");
            Field<NonNullGraphType<DateTimeGraphType>>("createdDate");
        }
    }
 
}
