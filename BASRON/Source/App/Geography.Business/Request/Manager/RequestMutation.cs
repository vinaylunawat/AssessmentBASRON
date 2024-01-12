using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Framework.DataAccess.Repository;
using BASRON.Business.Request.Models;
using BASRON.Business.GraphQL;
using BASRON.Business.GraphQL.Model;
using BASRON.Business.Request.Models;
using BASRON.Business.Request.Types;
using BASRON.Business.Request.Validator;
using BASRON.DataAccess;
using BASRON.DataAccess.Repository;
using BASRON.Entity.Entities;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using static GraphQL.Validation.BasicVisitor;

namespace BASRON.Business.Request.Manager
{
    public class RequestMutation : ITopLevelMutation
    {
        private readonly IRequestRepository _requestRepository;
        private readonly RequestCreateModelValidator _requestCreateValidator;
        private readonly RequestUpdateModelValidator _requestUpdateValidator;
        private readonly IMapper _mapper;
        public RequestMutation(IRequestRepository requestRepository, RequestCreateModelValidator requestCreateValidator, 
            RequestUpdateModelValidator requestUpdateValidator, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _requestCreateValidator = requestCreateValidator;
            _requestUpdateValidator = requestUpdateValidator;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<RequestType>("createRequest")
               .Argument<NonNullGraphType<RequestCreateInputType>>("request", "object of request")
               .ResolveAsync(async context => await ResolveCreateRequest(context).ConfigureAwait(false));

            graphType.Field<RequestType>("updateRequest")
                .Argument<NonNullGraphType<RequestUpdateInputType>>("request", "object of request")
                .ResolveAsync(async context => await ResolveUpdateRequest(context).ConfigureAwait(false));

            graphType.Field<StringGraphType>("deleteRequest")
            .Argument<NonNullGraphType<IdGraphType>>("referenceNumber", "id of request")
            .ResolveAsync(async context => await ResolveDeleteRequest(context).ConfigureAwait(false));
        }

        private async Task<RequestReadModel> ResolveCreateRequest(IResolveFieldContext<object> context)
        {
            var request = context.GetArgument<RequestCreateModel>("request");

            var validationResult = _requestCreateValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            var dbEntity = _mapper.Map<Entity.Entities.Request>(request);
            dbEntity.ReferenceNumber = Guid.NewGuid();
            var addedBTrasction = await _requestRepository.CreateAsync(dbEntity, default).ConfigureAwait(false);
            var result = _mapper.Map<RequestReadModel>(addedBTrasction);
            return result;

        }        

        private async Task<RequestReadModel> ResolveUpdateRequest(IResolveFieldContext<object> context)
        {
            var updateModel = context.GetArgument<RequestUpdateModel>("request");
            var validationResult = _requestUpdateValidator.Validate(updateModel);
            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            var dbEntity = await _requestRepository.GetByKey(updateModel.ReferenceNumber, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find request in db."));
                return null;
            }
            dbEntity.CreatedDate = DateTime.Now;
            dbEntity.Status = updateModel.Status;
            dbEntity.Remark = updateModel.Remark;
            var updatedRequest = await _requestRepository.UpdateAsync(dbEntity, default).ConfigureAwait(false);
            return _mapper.Map<RequestReadModel>(updatedRequest);
        }

        private async Task<object> ResolveDeleteRequest(IResolveFieldContext<object> context)
        {
            var requestId = context.GetArgument<Guid>("referenceNumber");
            var dbEntity = await _requestRepository.GetByKey(requestId, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find request in db."));
                return null;
            }

            await _requestRepository.DeleteAsync(requestId, default).ConfigureAwait(false);
            var res = new MutationResponse()
            {
                Message = $"The request with the id: {requestId} has been successfully deleted from db."
            };
            return res.Message;
        }

        private static void LoadErrors(IResolveFieldContext<object> context, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                context.Errors.Add(new ExecutionError(error.ErrorMessage));
            }
        }
    }
}
