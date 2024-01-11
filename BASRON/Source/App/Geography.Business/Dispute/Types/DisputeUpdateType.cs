using GraphQL.Types;
using System;

namespace BASRON.Business.Dispute.Types
{
    public class DisputeUpdateInputType : InputObjectGraphType
    {
        public DisputeUpdateInputType()
        {
            Name = "disputeUpdateInput";
            Field<IdGraphType>("Id");
            Field<NonNullGraphType<IdGraphType>>("customerId");
            Field<NonNullGraphType<StringGraphType>>("status");
            Field<NonNullGraphType<StringGraphType>>("remark");
        }
    }
 
}
