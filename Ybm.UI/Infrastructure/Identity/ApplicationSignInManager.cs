using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Infrastructure.Identity
{
    public class ApplicationSignInManager :SignInManager<CustomUser, string> 
    {

        public ApplicationSignInManager(ApplicationUserManager usermanager,
            IAuthenticationManager authenticationManager):
            base(usermanager,authenticationManager)
        {

        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager > options, IOwinContext context)
        {
            
            return new ApplicationSignInManager(context.Get<ApplicationUserManager>(),context.Authentication);
        }

    }
}