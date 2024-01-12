using BASRON.Business.BTransaction.Models;
using GraphQL.Types;
namespace BASRON.Business.BTransaction.Types
{
    public class BTransactionType : ObjectGraphType<BTransactionReadModel>
    {
        public BTransactionType()
        {
            Field(x => x.ReferenceNumber, type: typeof(IdGraphType)); 
            Field(x => x.CustomerId, type: typeof(StringGraphType));
            Field(x => x.TransactionDate, type: typeof(DateTimeGraphType));
            Field(x => x.Amount, type: typeof(DecimalGraphType));          
            Field(x => x.TransactionType, type: typeof(StringGraphType));
            Field(x => x.CustomerName, type: typeof(StringGraphType));   
            Field(x => x.ProductType, type: typeof(StringGraphType));
            Field(x => x.Currency, type: typeof(StringGraphType));
            Field(x => x.CreatedDate, type: typeof(DateTimeGraphType));
            Field(x => x.UpdatedDate, type: typeof(DateTimeGraphType));
            Field(x => x.IsActive, type: typeof(BooleanGraphType));
            Field(x => x.TotalSum, type: typeof(DecimalGraphType));
        }
    }

    public class TransactionFieldType : EnumerationGraphType<TransactionField>
    {
    }

    public enum TransactionField
    {
        ReferenceNumber,
        CustomerName,
        CustomerId,
        TransactionDate,
        Amount,
        TransactionType,
        ProductType,
        Currency,
        CreatedDate,
        UpdatedDate,
        IsActive
    }
}
