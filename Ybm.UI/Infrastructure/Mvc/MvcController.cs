using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Ybm.UI.Infrastructure.Identity;
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

namespace Ybm.UI.Infrastructure
{
    [Authorize]
    public abstract class MvcController : Controller
    {
        //IErrorLogBusiness errorLogBiz = ServiceFactory.CreateInstance<IErrorLogBusiness>();
        //IUserBusiness userBiz = ServiceFactory.CreateInstance<IUserBusiness>();
        //IMessageBusiness MessageBiz = ServiceFactory.CreateInstance<IMessageBusiness>();
        //IUserGroupTokenBusiness userGroupTokenBiz = ServiceFactory.CreateInstance<IUserGroupTokenBusiness>();
        //public int User_Id
        //{
        //    get
        //    {
        //        //return 1097;
        //        return GetCurrentUserId();
        //    }
        //}

        //public bool User_IsAuthenticated
        //{
        //    get
        //    {
        //        //return true;
        //        return User.Identity.IsAuthenticated;
        //    }
        //}
        //public User CurrentUser
        //{
        //    get
        //    {
        //        return GetCurrentUser();
        //    }
        //}
        //private int GetCurrentUserId()
        //{
        //    CustomUser user;
        //    if (Session["UserId"] == null)
        //    {
        //        var user_Id = User.Identity.GetUserId();
        //        user = userManager.FindById(user_Id);
        //        var us = userBiz.FirstOrDefault(x => x.Id == user.User_Id && x.IsVerified && x.VerificationDateTime.HasValue && x.IsActivated);
        //        if (us != null)
        //        {
        //            //Session["User"] = user;
        //            //Session["UserId"] = 4;// user.User_Id;
        //            Session["UserId"] = user.User_Id;
        //        }
        //    }
        //    return (int)Session["UserId"];
        //}
        //private User GetCurrentUser()
        //{
        //    // return userBiz.FirstOrDefault(x => x.Id == User_Id);
        //    return userBiz.FirstOrDefault(x => x.Id == User_Id && x.IsVerified && x.VerificationDateTime.HasValue && x.IsActivated);
        //}

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
        public async Task ResetPassword(string newPassword, string userName)
        {

            ApplicationDbContext context = new ApplicationDbContext();
            UserStore<CustomUser> store = new UserStore<CustomUser>(context);
            UserManager<CustomUser> UserManager = new UserManager<CustomUser>(store);
            String hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
            //CustomUser cUser = await store.FindByIdAsync(userId);
            CustomUser cUser = await store.FindByNameAsync(userName);
            await store.SetPasswordHashAsync(cUser, hashedNewPassword);
            await store.UpdateAsync(cUser);

            //-----------------------------

            //// reset password - first way
            //ApplicationDbContext context = new ApplicationDbContext();
            //UserStore<CustomUser> store = new UserStore<CustomUser>(context);
            //UserManager<CustomUser> UserManager = new UserManager<CustomUser>(store);
            //String userId = User.Identity.GetUserId();//"<YourLogicAssignsRequestedUserId>";
            //String newPassword = "test@123"; //"<PasswordAsTypedByUser>";
            //String hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
            //CustomUser cUser = await store.FindByIdAsync(userId);
            //await store.SetPasswordHashAsync(cUser, hashedNewPassword);
            //await store.UpdateAsync(cUser);

            //// reset password - second way
            //string resetToken = await userManager.GeneratePasswordResetTokenAsync(userId);
            //IdentityResult passwordChangeResult = await userManager.ResetPasswordAsync(userId, resetToken, "NewPassword");

            ////not recommended
            //userManager.RemovePassword(userId);
            //userManager.AddPassword(userId, newPassword);

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

        //public Action<Exception, ErrorType, int> SaveErrorLog = new Action<Exception, ErrorType, int>(saveErrorLog);
        //public static void  saveErrorLog(Exception ex, EnumErrorType errorType = EnumErrorType.General, int userId)
        //{
        //    var errorLog = new ErrorLog()
        //    {
        //        IsCustomError = false,
        //        Message = ex.Message,
        //        InnerException = ex.InnerException != null ? ex.InnerException.Message : "",
        //        StackTrace = ex.StackTrace,
        //        Source = ex.Source,
        //        IpAddress = HttpContext.Request.UserHostAddress,
        //        CreationDateTime = DateTime.Now,
        //        ErrorType_Id = (byte)errorType,
        //        User_Id = userId
        //    };

        //    errorLogBiz.Create(errorLog);


        //}

        //Action<int> action = new Action<int>(method1);

        //static void method1(int a)
        //{

        //}


        //Action<Exception, int?, byte?, string> action = method1;
        //new Action<Exception,int?, byte?,string>(method1);

        //static void method1(Exception ex, int? userId, byte? errorType, string ip)
        //{
        //    var errorLog = new ErrorLog()
        //    {
        //        IsCustomError = false,
        //        Message = ex.Message,
        //        InnerException = ex.InnerException != null ? ex.InnerException.Message : "",
        //        StackTrace = ex.StackTrace,
        //        Source = ex.Source,
        //        IpAddress = ip,
        //        CreationDateTime = DateTime.Now,
        //        ErrorType_Id = (byte)errorType,
        //        User_Id = userId
        //    };

        //    //errorLogBiz.Create(errorLog);
        //}
        //public void CreateAlert(int Sender_Id, int User_Id, string Subject, string Body, EnumAlertType AlertType)
        //{
            //Message alert = new Message()
            //{
            //    Sender_Id = Sender_Id,
            //    User_Id = User_Id,
            //    Subject = Subject,
            //    Body = Body,
            //    AlertType_Id = (int)AlertType,
            //    CreationDate = DateTime.Now

            //};
            //MessageBiz.Create(alert);

        //}



        public async Task AddClaims(int userGroup_Id, string userId)
        {
            //var userGroupTokens = userGroupTokenBiz.
            //           //FetchMulti(x => x.UserGroup_Id == user.UserGroup_Id)
            //           FetchMulti(x => x.UserGroup_Id == userGroup_Id)
            //           .Select(q => q.Token.Name)
            //           .ToList();

            ////var claims = await userManager.GetClaimsAsync(customUser.Id);
            //var claims = await userManager.GetClaimsAsync(userId);

            //foreach (var tokenName in userGroupTokens)
            //{
            //    if (!claims.Any(x => x.Type == "UserRight" && x.Value == tokenName))
            //    {
            //        var claim = new Claim("UserRight", tokenName);

            //        //await userManager.AddClaimAsync(customUser.Id, claim);
            //        await userManager.AddClaimAsync(userId, claim);
            //    }

            //}

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
        public string UserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }
    }
}