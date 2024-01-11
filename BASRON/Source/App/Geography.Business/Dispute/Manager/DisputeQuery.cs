using AutoMapper;
using BASRON.Business.GraphQL;
using BASRON.Business.Dispute.Models;
using BASRON.Business.Dispute.Types;
using BASRON.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BASRON.Business.Dispute.Manager
{
    public class DisputeQuery : ITopLevelQuery
    {
        private readonly IDisputeRepository _disputeRepository;
        private readonly IMapper _mapper;
        public DisputeQuery(IDisputeRepository disputeRepository, IMapper mapper)
        {
            _disputeRepository = disputeRepository;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<DisputeType>>("disputes")
            .ResolveAsync(async context => await ResolveDisputes().ConfigureAwait(false));

            graphType.Field<DisputeType>("dispute")
            .Argument<NonNullGraphType<IdGraphType>>("dispute", "id of the btrasction")
            .ResolveAsync(async context => await ResolveDispute(context).ConfigureAwait(false));
        }

        private async Task<IEnumerable<DisputeReadModel>> ResolveDisputes()
        {
            var dbBTrasction = await _disputeRepository.GetAll(default).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<DisputeReadModel>>(dbBTrasction);
        }

        private async Task<DisputeReadModel> ResolveDispute(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<Guid>("disputeId");
            if (key != Guid.Empty)
            {
                var dbDispute= await _disputeRepository.GetByKey(key, default).ConfigureAwait(false);
                return _mapper.Map<DisputeReadModel>(dbDispute);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }

    }
}
