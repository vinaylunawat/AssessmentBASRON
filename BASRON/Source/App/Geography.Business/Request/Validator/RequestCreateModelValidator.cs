using FluentValidation;
using BASRON.Business.Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASRON.Business.Request.Validator
{
    public class RequestCreateModelValidator : AbstractValidator<RequestCreateModel>
    {
        public RequestCreateModelValidator()
        {            
            RuleFor(x => x.ReferenceNumber).NotEmpty().WithMessage("Reference Number is required");


            RuleFor(x => x.Amount).NotEmpty().WithMessage("Transaction amount is required");

            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Transaction amount shoulbe numeric and greater than 0.");


            RuleFor(x => x.TransactionDate).NotEmpty().WithMessage("Transaction Date is Required.");

            RuleFor(x => x.TransactionDate).Must(BeAValidDate).WithMessage("Date Format is not correct.");


            RuleFor(x => x.Remark).NotEmpty().WithMessage("Remark is required");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}
