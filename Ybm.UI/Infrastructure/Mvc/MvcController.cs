using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ybm.Framework;
using Ybm.Business;
using System.Reflection;
using Ybm.UI.Infrastructure;
using Ybm.UI.Infrastructure.Authorization;
using System.Text;
using System.Threading.Tasks;
using Ybm.Common.Models;
using Ybm.Common;
using Kendo.Mvc;
using System.Security.Claims;
using Ybm.Framework.Identity2;

namespace Ybm.UI.Infrastructure
{
    [Authorize]
    public abstract class MvcController : Controller
    {
        IUserGroupTokenBusiness userGroupTokenBiz = ServiceFactory.CreateInstance<IUserGroupTokenBusiness>();

     
        public ApplicationUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
        }

        public ApplicationSignInManager signInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

        }

        public string GetIpAddress()
        {
            var ip = Request.UserHostAddress;
            return ip;
        }
       
        public string GenerateNewPassword()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            StringBuilder b = new StringBuilder();
            char ch;
            for (int i = 0; i < 6; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                b.Append(ch);


            }
            var newPassword = b.ToString();

            return newPassword;
        }

        public async Task AddClaims(int userGroup_Id, string userId)
        {
            var userGroupTokens = userGroupTokenBiz.
                       //FetchMulti(x => x.UserGroup_Id == user.UserGroup_Id)
                       FetchMulti(x => x.UserGroup_Id == userGroup_Id)
                       .Select(q => q.Token.Name)
                       .ToList();

            //var claims = await userManager.GetClaimsAsync(customUser.Id);
            var claims = await userManager.GetClaimsAsync(userId);

            foreach (var tokenName in userGroupTokens)
            {
                if (!claims.Any(x => x.Type == "UserRight" && x.Value == tokenName))
                {
                    var claim = new Claim("UserRight", tokenName);

                    //await userManager.AddClaimAsync(customUser.Id, claim);
                    await userManager.AddClaimAsync(userId, claim);
                }

            }

        }

        public void RemoveClaim(string userId, string claimType, string claimValue)
        {
            //var claim = new Claim(claimType, claimValue);
            //await userManager.RemoveClaimAsync(userId, claim);

            var claims = userManager.GetClaims(userId);

            if (claims.Any(x => x.Type == claimType && x.Value == claimValue))
            {
                var claim = new Claim(claimType, claimValue);
                userManager.RemoveClaim(userId, claim);
            }
        }

        public void AddClaim(string userId, string claimType, string claimValue)
        {
            var claims = userManager.GetClaims(userId);

            if (!claims.Any(x => x.Type == claimType && x.Value == claimValue))
            {
                var claim = new Claim(claimType, claimValue);
                userManager.AddClaim(userId, claim);
            }


        }
        public async Task ResetPassword(string newPassword, string userName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            UserStore<CustomUser> store = new UserStore<CustomUser>(context);
            UserManager<CustomUser> UserManager = new UserManager<CustomUser>(store);
            String hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
            CustomUser cUser = await store.FindByNameAsync(userName);
            await store.SetPasswordHashAsync(cUser, hashedNewPassword);
            await store.UpdateAsync(cUser);
        }


        public string UserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }
    }
}