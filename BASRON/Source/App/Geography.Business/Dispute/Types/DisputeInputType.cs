using GraphQL.Types;
using System;

namespace BASRON.Business.Dispute.Types
{
    public class DisputeCreateInputType : InputObjectGraphType
    {
        public DisputeCreateInputType()
        {
            Name = "disputeCreateInput";
            Field<NonNullGraphType<IdGraphType>>("customerId");
            Field<NonNullGraphType<DateGraphType>>("transactionDate");
            Field<NonNullGraphType<IdGraphType>>("referenceNumber");
            Field<NonNullGraphType<StringGraphType>>("customerName");
            Field<NonNullGraphType<DecimalGraphType>>("amount");
            Field<NonNullGraphType<StringGraphType>>("status");
            Field<NonNullGraphType<StringGraphType>>("remark");
            Field<NonNullGraphType<StringGraphType>>("transactionType");
           
            
            //Field<NonNullGraphType<BooleanGraphType>>("isSync");
            //Field<IdGraphType>("countryId");
        }
    } 
}
