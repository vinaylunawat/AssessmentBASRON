using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Framework.DataAccess.Repository;
using BASRON.Business.GraphQL;
using BASRON.Business.GraphQL.Model;
using BASRON.Business.BTransaction.Models;
using BASRON.Business.BTransaction.Types;
using BASRON.Business.BTransaction.Validator;
using BASRON.DataAccess;
using BASRON.DataAccess.Repository;
using BASRON.Entity.Entities;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using static GraphQL.Validation.BasicVisitor;

namespace BASRON.Business.BTransaction.Manager
{
    public class BTransactionMutation : ITopLevelMutation
    {
        private readonly IBTransactionRepository _btransactionRepository;
        private readonly BTransactionCreateModelValidator _btransactionCreateValidator;
        private readonly BTransactionUpdateModelValidator _btransactionUpdateValidator;
        private readonly IMapper _mapper;
        public BTransactionMutation(IBTransactionRepository btransactionRepository, BTransactionCreateModelValidator btransactionCreateValidator, 
            BTransactionUpdateModelValidator btransactionUpdateValidator, IMapper mapper)
        {
            _btransactionRepository = btransactionRepository;
            _btransactionCreateValidator = btransactionCreateValidator;
            _btransactionUpdateValidator = btransactionUpdateValidator;
            _mapper = mapper;
        }
        public void RegisterField(ObjectGraphType graphType)
        {
            graphType.Field<BTransactionType>("createBTransaction")
               .Argument<NonNullGraphType<BTransactionCreateInputType>>("btransaction", "object of btrasction")
               .ResolveAsync(async context => await ResolveCreateTransaction(context).ConfigureAwait(false));

            graphType.Field<BTransactionType>("updateBTransaction")
                .Argument<NonNullGraphType<BTransactionUpdateInputType>>("btransaction", "object of btrasction")
                .ResolveAsync(async context => await ResolveUpdateTransaction(context).ConfigureAwait(false));

            graphType.Field<StringGraphType>("deleteBTransaction")
            .Argument<NonNullGraphType<IdGraphType>>("btransactionId", "id of btrasction")
            .ResolveAsync(async context => await ResolveDeleteTransaction(context).ConfigureAwait(false));

            
        }

        private async Task<BTransactionReadModel> ResolveCreateTransaction(IResolveFieldContext<object> context)
        {
            var btrasction = context.GetArgument<BTransactionCreateModel>("btransaction");

            var validationResult = _btransactionCreateValidator.Validate(btrasction);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

         
            try
            {
                var dbEntity = _mapper.Map<Entity.Entities.BTransaction>(btrasction);
                dbEntity.ReferenceNumber = Guid.NewGuid();
                var addedBTrasction = await _btransactionRepository.CreateAsync(dbEntity, default).ConfigureAwait(false);
                var result = _mapper.Map<BTransactionReadModel>(addedBTrasction);
                return result;

            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
            return null;

        }


        private async Task<BTransactionReadModel> ResolveUpdateTransaction(IResolveFieldContext<object> context)
        {

            var btrasctionUpdateModel = context.GetArgument<BTransactionUpdateModel>("btransaction");

            var validationResult = _btransactionUpdateValidator.Validate(btrasctionUpdateModel);

            if (!validationResult.IsValid)
            {
                LoadErrors(context, validationResult);
                return null;
            }

            var dbEntity = await _btransactionRepository.GetByKey(btrasctionUpdateModel.ReferenceNumber, default).ConfigureAwait(false);
            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find btrasction in db."));
                return null;
            }
            var dbBTrasction = _mapper.Map<Entity.Entities.BTransaction>(btrasctionUpdateModel);
            var updatedBTrasction = await _btransactionRepository.UpdateAsync(dbBTrasction, default).ConfigureAwait(false);
            return _mapper.Map<BTransactionReadModel>(updatedBTrasction);

        }

        private async Task<object> ResolveDeleteTransaction(IResolveFieldContext<object> context)
        {

            var btrasctionId = context.GetArgument<Guid>("btransactionId");

            var dbEntity = await _btransactionRepository.GetByKey(btrasctionId, default).ConfigureAwait(false);

            if (dbEntity == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find btrasction in db."));
                return null;
            }

            await _btransactionRepository.DeleteAsync(btrasctionId, default).ConfigureAwait(false);

            var res = new MutationResponse()
            {
                Message = $"The btrasction with the id: {btrasctionId} has been successfully deleted from db."
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
