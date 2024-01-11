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
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer is required");

            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Customer Name is required");

            RuleFor(x => x.TransactionDate).NotEmpty().WithMessage("Transaction Date is required");

            RuleFor(x => x.ReferenceNumber).NotEmpty().WithMessage("Reference Number is required");

            RuleFor(x => x.Amount).NotEmpty().WithMessage("Transaction amount is required");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");

            RuleFor(x => x.Remark).NotEmpty().WithMessage("Remark is required");

            RuleFor(x => x.TransactionType).NotEmpty().WithMessage("Transaction Type is required");

        }

    }
}
