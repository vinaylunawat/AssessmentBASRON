using AutoMapper;
using BASRON.Business.GraphQL;
using BASRON.Business.Request.Models;
using BASRON.Business.Request.Types;
using BASRON.DataAccess.Repository;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
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
            .Argument<NonNullGraphType<IdGraphType>>("request", "id of the btransaction")
            .ResolveAsync(async context => await ResolveRequest(context).ConfigureAwait(false));

            graphType.Field<ListGraphType<RequestType>>("SearchRequestByAttributes")
                 .Argument<RequestSearchType>("requestSearch", "object of request")
          .ResolveAsync(async context => await ResolveSearchRequestByAttributes(context).ConfigureAwait(false));
        }

        private async Task<IEnumerable<RequestReadModel>> ResolveRequests()
        {
            var dbBTransaction = await _requestRepository.GetAll(default).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<RequestReadModel>>(dbBTransaction);
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

        private async Task<IEnumerable<RequestReadModel>> ResolveSearchRequestByAttributes(IResolveFieldContext<object> context)
        {
            var requestData = context.GetArgument<RequestReadModel>("requestData");

            var dbEntity = _mapper.Map<Entity.Entities.Request>(requestData);

            Dictionary<string, string> data = new Dictionary<string, string>();

            var filterAttrbutes = context.Variables.Select(x => x.Value).ToList();

            foreach (var entity in filterAttrbutes)
            {
                var keysData = ((Dictionary<string, object>)entity);
                var listOfKeys = ((Dictionary<string, object>)entity);

                foreach (var key in listOfKeys)
                {
                    if (Enum.TryParse<RequestField>(key.Key, true, out var actualFieldValue))
                    {
                        data.Add(actualFieldValue.ToString(), Convert.ToString(key.Value));
                    }
                }
            }
            try
            {
                var dbRequest = await _requestRepository.GetPaginatedScanItemsAsync(data).ConfigureAwait(false);
                var res = _mapper.Map<IEnumerable<RequestReadModel>>(dbRequest);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
