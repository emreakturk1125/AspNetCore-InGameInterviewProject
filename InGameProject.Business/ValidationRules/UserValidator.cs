using FluentValidation;
using InGameProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InGameProject.Business.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserEmail).NotEmpty().WithMessage("Mail Adresini Boş Geçemezsiniz!");
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("Şifreyi Boş Geçemezsiniz!");
        }
    }
}
