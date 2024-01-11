using BASRON.Business.BTransaction.Models;
using GraphQL.Types;
namespace BASRON.Business.BTransaction.Types
{
    public class BTransactionType : ObjectGraphType<BTransactionReadModel>
    {
        public BTransactionType()
        {
            Field(x => x.ReferenceNumber, type: typeof(IdGraphType)); 
            Field(x => x.CustomerId, type: typeof(IdGraphType));
            Field(x => x.TransactionDate, type: typeof(DateGraphType));
            Field(x => x.Amount, type: typeof(DecimalGraphType));          
            Field(x => x.TransactionType, type: typeof(StringGraphType));
            Field(x => x.ProductType, type: typeof(StringGraphType));
            Field(x => x.Currency, type: typeof(StringGraphType));
            Field(x => x.CreatedDate, type: typeof(DateGraphType));
            Field(x => x.UpdatedDate, type: typeof(DateGraphType));
            Field(x => x.IsActive, type: typeof(BooleanGraphType));
        }
    }
}
