using FluentValidation;
using BASRON.Business.BTransaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASRON.Business.BTransaction.Validator
{
    public class BTransactionCreateModelValidator : AbstractValidator<BTransactionCreateModel>
    {
        public BTransactionCreateModelValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer is required");

            RuleFor(x => x.TransactionDate).NotEmpty().WithMessage("Transaction Date is required");

            RuleFor(x => x.ReferenceNumber).NotEmpty().WithMessage("Reference Number is required");

            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required");

            RuleFor(x => x.TransactionType).NotEmpty().WithMessage("Transaction Type is required");

            RuleFor(x => x.ProductType).NotEmpty().WithMessage("Product Type is required");

            RuleFor(x => x.Currency).NotEmpty().WithMessage("Currency is required");

            RuleFor(x => x.CreatedDate).NotEmpty().WithMessage("Created Date is required");

        }

    }
}
