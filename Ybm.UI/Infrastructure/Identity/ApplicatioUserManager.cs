using Ybm.UI.Infrastructure.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Ybm.UI.Infrastructure.Identity
{
    public class ApplicationUserManager : UserManager<CustomUser>
    {

     
        public ApplicationUserManager(UserStore<CustomUser> userStore)
            :base(userStore) {        }

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options ,IOwinContext context) 
        //{
        //    var userManager = new UserStore<CustomUser>(context.Get<ApplicationDbContext>());
        //    return new ApplicationUserManager(userManager);
        //}

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



            //var manager = new ApplicationUserManager(new UserStore<CustomUser>(context.Get<ApplicationDbContext>()));
            //manager.UserValidator = new CustomUserValidator<CustomUser>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};
            ////var userManager = new UserStore<CustomUser>(context.Get<ApplicationDbContext>());
            
            //return new ApplicationUserManager(manager);
        }

    }
}