using GraphQL.Types;
using System;

namespace BASRON.Business.Request.Types
{
    public class RequestUpdateInputType : InputObjectGraphType
    {
        public RequestUpdateInputType()
        {
            Name = "RequestUpdateInput";
            Field<IdGraphType>("Id");
            Field<NonNullGraphType<IdGraphType>>("customerId");
            Field<NonNullGraphType<StringGraphType>>("status");
            Field<NonNullGraphType<StringGraphType>>("remark");
        }
    }
 
}
