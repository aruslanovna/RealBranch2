using Microsoft.AspNet.Identity;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace RealBranch.Infrastructure
{

    public class CustomUserValidator : UserValidator<AppUser>
    {

        public CustomUserValidator(AppUserManager manager)
          : base(manager)
        { }

        public override async Task<IdentityResult> ValidateAsync(AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@mail.com") || !user.Email.ToLower().EndsWith("@gmail.com") || !user.Email.ToLower().EndsWith("@nure.ua"))
            {
                var errors = result.Errors.ToList();
                errors.Add(Resources.Content.emailInvalid);
                result = new IdentityResult(errors);
            }
            List<string> error = new List<string>();
            if (String.IsNullOrEmpty(user.UserName.Trim()))
                error.Add(Resources.Content.EmptyName);

            string userNamePattern = @"^[a-zA-Z]+$";

            if (!Regex.IsMatch(user.UserName, userNamePattern))
                error.Add(Resources.Content.InvaliName);

            if (error.Count > 0)
                return IdentityResult.Failed(error.ToArray());

            return result;
        }
    }
}

