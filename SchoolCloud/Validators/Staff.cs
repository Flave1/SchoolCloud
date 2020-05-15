using FluentValidation;
using SchoolCloud.Contract.Commands;
using SchoolCloud.Contract.RequestObjs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCloud.Validators
{
    public class UdateStaffCommandValidator : AbstractValidator<UdateStaffCommand> {
        public UdateStaffCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.MiddleName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.SurName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.FristName).NotEmpty().MinimumLength(2);
        } 
    }  
}
