using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Framework.DataAccess.Repository;
using BASRON.Business.Dispute.Models;
using BASRON.Business.GraphQL;
using BASRON.Business.GraphQL.Model;
using BASRON.Business.Dispute.Models;
using BASRON.Business.Dispute.Types;
using BASRON.Business.Dispute.Validator;
using BASRON.DataAccess;
using BASRON.DataAccess.Repository;
using BASRON.Entity.Entities;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using static GraphQL.Validation.BasicVisitor;

namespace BASRON.Business.Dispute.Manager
{
    public class DisputeMutation : ITopLevelMutation
    {
        private readonly IDisputeRepository _disputeRepository;
        private readonly DisputeCreateModelValidator _disputeCreateValidator;
        private readonly DisputeUpdateModelValidator _disputeUpdateValidator;
        private readonly IMapper _mapper;
        public DisputeMutation(IDisputeRepository bDisputeRepository, DisputeCreateModelValidator disputeCreateValidator, 
            DisputeUpdateModelValidator disputeUpdateValidator, IMapper mapper)
        {
            _disputeRepository = bDisputeRepository;
            _disputeCreateValidator = disputeCreateValidator;
            _disputeUpdateValidator = disputeUpdateValidator;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<DisputeType>("createDispute")
               .Argument<NonNullGraphType<DisputeCreateInputType>>("dispute", "object of dispute")
               .ResolveAsync(async context => await ResolveCreateDispute(context).ConfigureAwait(false));

            graphType.Field<DisputeType>("updateDispute")
                .Argument<NonNullGraphType<DisputeUpdateInputType>>("dispute", "object of dispute")
                .ResolveAsync(async context => await ResolveUpdateDispute(context).ConfigureAwait(false));

            graphType.Field<StringGraphType>("deleteDispute")
            .Argument<NonNullGraphType<IdGraphType>>("disputeId", "id of dispute")
            .ResolveAsync(async context => await ResolveDeleteDispute(context).ConfigureAwait(false));
        }

        private async Task<DisputeReadModel> ResolveCreateDispute(IResolveFieldContext<object> context)
        {
            var dispute = context.GetArgument<DisputeCreateModel>("dispute");

            var validationResult = _disputeCreateValidator.Validate(dispute);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            var dbEntity = _mapper.Map<Entity.Entities.Dispute>(dispute);
            dbEntity.ReferenceNumber = Guid.NewGuid();
            var addedBTrasction = await _disputeRepository.CreateAsync(dbEntity, default).ConfigureAwait(false);
            var result = _mapper.Map<DisputeReadModel>(addedBTrasction);
            return result;

        }
        

        private async Task<DisputeReadModel> ResolveUpdateDispute(IResolveFieldContext<object> context)
        {

            var disputeUpdateModel = context.GetArgument<DisputeUpdateModel>("dispute");

            var validationResult = _disputeUpdateValidator.Validate(disputeUpdateModel);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            var dbEntity = await _disputeRepository.GetByKey(disputeUpdateModel.Id, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find dispute in db."));
                return null;
            }
            var dbDispute = _mapper.Map<Entity.Entities.Dispute>(disputeUpdateModel);
            var updatedDispute = await _disputeRepository.UpdateAsync(dbDispute, default).ConfigureAwait(false);
            return _mapper.Map<DisputeReadModel>(updatedDispute);

        }

        private async Task<object> ResolveDeleteDispute(IResolveFieldContext<object> context)
        {
            var disputeId = context.GetArgument<Guid>("disputeId");

            var dbEntity = await _disputeRepository.GetByKey(disputeId, default).ConfigureAwait(false);

            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find dispute in db."));
                return null;
            }

            await _disputeRepository.DeleteAsync(disputeId, default).ConfigureAwait(false);

            var res = new MutationResponse()
            {
                Message = $"The dispute with the id: {disputeId} has been successfully deleted from db."
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
