using FluentValidation;
using BASRON.Business.Dispute.Models;

namespace BASRON.Business.Dispute.Validator
{
    public class DisputeUpdateModelValidator : AbstractValidator<DisputeUpdateModel>
    {
        public DisputeUpdateModelValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer is required");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");

            RuleFor(x => x.Remark).NotEmpty().WithMessage("Remark is required");
        }
    }
}
