using FluentValidation;
using SchoolCloud.Contract.RequestObjs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Validators
{
    public class BurserRegstrationReqObjValidator : AbstractValidator<BurserRegstrationReqObj>
    {
        public BurserRegstrationReqObjValidator()
        {
            RuleFor(x => x.UserType).NotEmpty().NotNull().WithMessage("User type is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("User type is required");
        }
    } 
}
