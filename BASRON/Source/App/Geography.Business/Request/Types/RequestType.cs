using BASRON.Business.Request.Models;
using GraphQL.Types;

namespace BASRON.Business.Request.Types
{
    public class RequestType : ObjectGraphType<RequestReadModel>
    {
        public RequestType()
        {
            Field(x => x.CustomerId, type: typeof(StringGraphType));
            Field(x => x.TransactionDate, type: typeof(DateGraphType));
            Field(x => x.ReferenceNumber, type: typeof(IdGraphType));
            Field(x => x.Amount, type: typeof(DecimalGraphType));
            Field(x => x.Status, type: typeof(StringGraphType));
            Field(x => x.CustomerName, type: typeof(StringGraphType));
            Field(x => x.Remark, type: typeof(StringGraphType));
            Field(x => x.TransactionType, type: typeof(StringGraphType));          
        }
    }
}
