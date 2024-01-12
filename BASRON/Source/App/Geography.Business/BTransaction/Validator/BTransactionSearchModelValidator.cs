using FluentValidation;
using BASRON.Business.BTransaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASRON.Business.BTransaction.Validator
{
    public class BTransactionSearchModelValidator : AbstractValidator<BTransactionReadModel>
    {
        public BTransactionSearchModelValidator()
        {

        }

    }
}
