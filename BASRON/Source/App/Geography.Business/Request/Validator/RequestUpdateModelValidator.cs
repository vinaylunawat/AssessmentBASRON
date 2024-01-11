using FluentValidation;
using BASRON.Business.Request.Models;

namespace BASRON.Business.Request.Validator
{
    public class RequestUpdateModelValidator : AbstractValidator<RequestUpdateModel>
    {
        public RequestUpdateModelValidator()
        {
            RuleFor(x => x.ReferenceNumber).NotEmpty().WithMessage("Customer is required");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");

            RuleFor(x => x.Remark).NotEmpty().WithMessage("Remark is required");
        }
    }
}
