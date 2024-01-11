using AutoMapper;
using BASRON.Business.GraphQL;
using BASRON.Business.BTransaction.Models;
using BASRON.Business.BTransaction.Types;
using BASRON.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BASRON.Business.BTransaction.Manager
{
    public class BTransactionQuery : ITopLevelQuery
    {
        private readonly IBTransactionRepository _btransactionRepository;
        private readonly IMapper _mapper;
        public BTransactionQuery(IBTransactionRepository btransactionRepository, IMapper mapper)
        {
            _btransactionRepository = btransactionRepository;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<BTransactionType>>("bTransactions")
            .ResolveAsync(async context => await ResolveBTransactions().ConfigureAwait(false));

            graphType.Field<BTransactionType>("bTransaction")
            .Argument<IdGraphType>("btransactionId", "id of the btransaction")
            .ResolveAsync(async context => await ResolveBTransaction(context).ConfigureAwait(false));

            graphType.Field<BTransactionType>("GetCustomerTransactionByCustomerId")
           .Argument<IdGraphType>("customerId", "id of the btransaction")
           .ResolveAsync(async context => await ResolveGetTransactionByAttributes(context).ConfigureAwait(false));

        }

        private async Task<IEnumerable<BTransactionReadModel>> ResolveBTransactions()
        {
            var dbBTransaction = await _btransactionRepository.GetAll(default).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<BTransactionReadModel>>(dbBTransaction);
        }

        private async Task<BTransactionReadModel> ResolveBTransaction(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<Guid>("btransactionId");
            if (key != Guid.Empty)
            {
                var dbBTrasction = await _btransactionRepository.GetByKey(key, default).ConfigureAwait(false);
                return _mapper.Map<BTransactionReadModel>(dbBTrasction);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }

        private async Task<BTransactionReadModel> ResolveGetTransactionByAttributes(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<Guid>("btransactionId");
           // var key = context.Get<Guid>("btransactionId");

            if (key != Guid.Empty)
            {
                var dbBTrasction = await _btransactionRepository.GetByKey(key, default).ConfigureAwait(false);
                return _mapper.Map<BTransactionReadModel>(dbBTrasction);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }

    }
}
