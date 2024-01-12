using BASRON.Business.Request.Models;
using GraphQL.Types;

namespace BASRON.Business.Request.Types
{
    public class RequestType : ObjectGraphType<RequestReadModel>
    {
        public RequestType()
        {
            Field(x => x.CustomerId, type: typeof(StringGraphType));
            Field(x => x.TransactionDate, type: typeof(DateTimeGraphType));
            Field(x => x.ReferenceNumber, type: typeof(IdGraphType));
            Field(x => x.Amount, type: typeof(DecimalGraphType));
            Field(x => x.Status, type: typeof(StringGraphType));
            Field(x => x.CustomerName, type: typeof(StringGraphType));
            Field(x => x.Remark, type: typeof(StringGraphType));
            Field(x => x.TransactionType, type: typeof(StringGraphType));
            Field(x => x.TotalSum, type: typeof(DecimalGraphType));
            Field(x => x.CreatedDate, type: typeof(DateTimeGraphType));
            Field(x => x.IsActive, type: typeof(BooleanGraphType));

        }
    }

    public class RequestFieldType : EnumerationGraphType<RequestField>
    {
    }

    public enum RequestField
    {
        ReferenceNumber,
        CustomerName,
        CustomerId,
        TransactionDate,
        Amount,
        Status,
        Remark,
        TransactionType
    }
}
