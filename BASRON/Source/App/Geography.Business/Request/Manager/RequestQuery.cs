using AutoMapper;
using BASRON.Business.GraphQL;
using BASRON.Business.Request.Models;
using BASRON.Business.Request.Types;
using BASRON.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BASRON.Business.Request.Manager
{
    public class RequestQuery : ITopLevelQuery
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        public RequestQuery(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<ListGraphType<RequestType>>("requests")
            .ResolveAsync(async context => await ResolveRequests().ConfigureAwait(false));

            graphType.Field<RequestType>("request")
            .Argument<NonNullGraphType<IdGraphType>>("request", "id of the btrasction")
            .ResolveAsync(async context => await ResolveRequest(context).ConfigureAwait(false));
        }

        private async Task<IEnumerable<RequestReadModel>> ResolveRequests()
        {
            var dbBTrasction = await _requestRepository.GetAll(default).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<RequestReadModel>>(dbBTrasction);
        }

        private async Task<RequestReadModel> ResolveRequest(IResolveFieldContext<object> context)
        {
            var key = context.GetArgument<Guid>("requestId");
            if (key != Guid.Empty)
            {
                var result= await _requestRepository.GetByKey(key, default).ConfigureAwait(false);
                return _mapper.Map<RequestReadModel>(result);
            }
            else
            {
                context.Errors.Add(new ExecutionError("Wrong value for id"));
                return null;
            }
        }

    }
}
