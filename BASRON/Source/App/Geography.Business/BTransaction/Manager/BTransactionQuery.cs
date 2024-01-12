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
using BASRON.Business.BTransaction.Validator;
using FluentValidation;
using BASRON.Entity.Entities;
using GraphQL.Validation;
using System.Linq;
using LinqKit;
using Amazon.Runtime.Internal.Transform;
using Newtonsoft.Json.Linq;

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

            graphType.Field<ListGraphType<BTransactionType>>("GetCustomerTransactionByAttributes")
                  .Argument<BTransactionSearchType>("btransaction", "object of btransaction")
           .ResolveAsync(async context => await ResolveGetTransactionByAttributes(context).ConfigureAwait(false));

        }

        private async Task<IEnumerable<BTransactionReadModel>> ResolveBTransactions()
        {
            var dbBTransaction = await _btransactionRepository.GetAll(default).ConfigureAwait(false);
            var mappedData = _mapper.Map<IEnumerable<BTransactionReadModel>>(dbBTransaction);
            var sumData = mappedData.Sum(a => a.Amount);
            mappedData.ForEach(x => x.TotalSum = sumData);
            return mappedData;
        }

        private async Task<BTransactionReadModel> ResolveBTransaction(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<Guid>("btransactionId");
            if (key != Guid.Empty)
            {
                var dbBTransaction = await _btransactionRepository.GetByKey(key, default).ConfigureAwait(false);
                var mappedData = _mapper.Map<BTransactionReadModel>(dbBTransaction);
                mappedData.TotalSum = mappedData.Amount;
                return mappedData;
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }

        private async Task<IEnumerable<BTransactionReadModel>> ResolveGetTransactionByAttributes(IResolveFieldContext<object> context)
        {
            var btransaction = context.GetArgument<BTransactionReadModel>("btransaction");

            var dbEntity = _mapper.Map<Entity.Entities.BTransaction>(btransaction);

            Dictionary<string, string> data = new Dictionary<string, string>();

            var filterAttrbutes = context.Variables.Select(x => x.Value).ToList();

            foreach (var entity in filterAttrbutes)
            {
                var keysData = ((Dictionary<string, object>)entity);
                var listOfKeys = ((Dictionary<string, object>)entity);

                foreach (var key in listOfKeys)
                {
                    if (Enum.TryParse<TransactionField>(key.Key, true, out var actualFieldValue))
                    {
                        data.Add(actualFieldValue.ToString(), Convert.ToString(key.Value));
                    }
                }
            }
            try
            {
                var dbBTransaction = await _btransactionRepository.GetPaginatedScanItemsAsync(data).ConfigureAwait(false);
                var res = _mapper.Map<IEnumerable<BTransactionReadModel>>(dbBTransaction);
                return res;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
