using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Ybm.Framework.Identity2
{
    public class ApplicationUserManager : UserManager<CustomUser>
    {

     
        public ApplicationUserManager(UserStore<CustomUser> userStore)
            :base(userStore) {        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<CustomUser>(context.Get<ApplicationDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<CustomUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

           
            return manager;
        }
    }
}