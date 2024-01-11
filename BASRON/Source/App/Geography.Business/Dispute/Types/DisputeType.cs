using BASRON.Business.Dispute.Models;
using GraphQL.Types;

namespace BASRON.Business.Dispute.Types
{
    public class DisputeType : ObjectGraphType<DisputeReadModel>
    {
        public DisputeType()
        {
            Field(x => x.CustomerId, type: typeof(IdGraphType));
            Field(x => x.TransactionDate, type: typeof(DateGraphType));
            Field(x => x.ReferenceNumber, type: typeof(IdGraphType));
            Field(x => x.Amount, type: typeof(DecimalGraphType));
            Field(x => x.Status, type: typeof(StringGraphType));
            Field(x => x.CustomerName, type: typeof(StringGraphType));
            Field(x => x.Remark, type: typeof(StringGraphType));
            Field(x => x.TransactionType, type: typeof(StringGraphType));
            //Field<CountryType>("Country");
            //.ResolveAsync(async context => await ResolveBTrasctionsAsync(context));

        }
    }
}
