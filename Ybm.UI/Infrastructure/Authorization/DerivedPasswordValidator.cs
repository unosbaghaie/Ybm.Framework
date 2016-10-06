using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Ybm.UI.Infrastructure.Authorization
{
    public class DerivedPasswordValidator : PasswordValidator
    {
        public override Task<IdentityResult> ValidateAsync(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(item) || item.Length < RequiredLength)
            {
                //TODO: add error message from your own resources file with specified culture.
                //TODO: remove following line if your code completed
                errors.Add("String.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, RequiredLength)");
            }
            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
            {
                //TODO: add error message from your own resources file with specified culture.
                //TODO: remove following line if your code completed
                errors.Add("Resources.PasswordRequireNonLetterOrDigit");
            }
            if (RequireDigit && item.All(c => !IsDigit(c)))
            {
                //TODO: add error message from your own resources file with specified culture.
                //TODO: remove following line if your code completed
                errors.Add("Resources.PasswordRequireDigit");
            }
            if (RequireLowercase && item.All(c => !IsLower(c)))
            {
                //TODO: add error message from your own resources file with specified culture.
                //TODO: remove following line if your code completed
                errors.Add("Resources.PasswordRequireLower");
            }
            if (RequireUppercase && item.All(c => !IsUpper(c)))
            {
                //TODO: add error message from your own resources file with specified culture.
                //TODO: remove following line if your code completed
                errors.Add("Resources.PasswordRequireUpper");
            }
            if (errors.Count == 0)
            {
                //TODO: add error message from your own resources file with specified culture.
                //TODO: remove following line if your code completed
                //return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(String.Join(" ", errors)));
        }
    }

    /// <summary>
    ///     Validates users before they are saved to an IUserStore
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class CustomUserValidator<TUser> : UserValidator<TUser, string>
        where TUser : IdentityUser
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="manager"></param>
        public CustomUserValidator(UserManager<TUser, string> manager) : base(manager)
        {
            this.Manager = manager;
        }

        private UserManager<TUser, string> Manager { get; set; }

        /// <summary>
        ///     Validates a user before saving
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> ValidateAsync(TUser item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var errors = new List<string>();
            await ValidateUserName(item, errors);
            if (RequireUniqueEmail)
            {
                await ValidateEmail(item, errors);
            }
            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }
            return IdentityResult.Success;
        }

        private async Task ValidateUserName(TUser user, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add("String.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, Name)");
            }
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, @"^[A-Za-z0-9@_\.]+$"))
            {
                // If any characters are not letters or digits, its an illegal user name
                errors.Add("String.Format(CultureInfo.CurrentCulture, Resources.InvalidUserName, user.UserName)");
            }
            else
            {
                var owner = await Manager.FindByNameAsync(user.UserName);
                if (owner != null && !EqualityComparer<string>.Default.Equals(owner.Id, user.Id))
                {
                    errors.Add("String.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, user.UserName)");
                }
            }
        }

        // make sure email is not empty, valid, and unique
        private async Task ValidateEmail(TUser user, List<string> errors)
        {
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    errors.Add("String.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, Email)");
                    return;
                }
                try
                {
                    var m = new MailAddress(user.Email);
                }
                catch (FormatException)
                {
                    errors.Add("String.Format(CultureInfo.CurrentCulture, Resources.InvalidEmail, email)");
                    return;
                }
            }
            var owner = await Manager.FindByEmailAsync(user.Email);
            if (owner != null && !EqualityComparer<string>.Default.Equals(owner.Id, user.Id))
            {
                errors.Add("String.Format(CultureInfo.CurrentCulture, Resources.DuplicateEmail, email)");
            }
        }
    }
}