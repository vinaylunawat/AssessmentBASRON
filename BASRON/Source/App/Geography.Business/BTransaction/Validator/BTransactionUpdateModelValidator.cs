using FluentValidation;
using BASRON.Business.BTransaction.Models;

namespace BASRON.Business.BTransaction.Validator
{
    public class BTransactionUpdateModelValidator : AbstractValidator<BTransactionUpdateModel>
    {
        public BTransactionUpdateModelValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        }

    }
}
