using Ybm.Business;
using Ybm.Framework;
using Ybm.UI.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ybm.UI.Infrastructure.Authorization;
using Ybm.Common.Models;
using System.Net;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Ybm.Framework.Identity2;
using Ybm.Framework.PersianCaptchaHandler;
using Ybm.Common;
using Ybm.UI.Infrastructure.ViewModels;

namespace Ybm.UI.Controllers
{
    [TokenCategory(CategoryTitle = "صفحه مدیریت حساب کاربری", CategoryName = "AccountPage")]
    public class AccountController : MvcController
    {

        IUserBusiness userBiz = ServiceFactory.CreateInstance<IUserBusiness>();
        IUserGroupTokenBusiness userGroupTokenBiz = ServiceFactory.CreateInstance<IUserGroupTokenBusiness>();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        #region [ورود کاربر]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> LoginUser(LoginViewModel model)
        {

            var isLogin = false;
            var message = string.Empty;
            var url = string.Empty;
            string strUserHashKey = string.Empty;

            var response = Request["g-recaptcha-response"];

            //if (response != null && IsValidCaptcha(response))

            if (ModelState.IsValid)
            {
                var customUser = signInManager.UserManager.Find(model.UserName, model.Password);

                var user = customUser != null ? userBiz.FirstOrDefault(x => x.Id == customUser.User_Id) : null;

                var errorMessage = getloginFailureReasonMessage(user);
                //todo:(mr baghie)show errors when login failed
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError("", errorMessage);
                    return View(model);
                    // throw new Exception(errorMessage);
                }
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            var ident = userManager.CreateIdentity(customUser, DefaultAuthenticationTypes.ApplicationCookie);

                            #region [add claims]

                            var userGroupTokens = userGroupTokenBiz.
                               FetchMulti(x => x.UserGroup_Id == user.UserGroup_Id)
                               .Select(q => q.Token.Name)
                               .ToList();


                            var claimList = ident.Claims.Where(x => x.Type == "UserRight").Select(x => x.Value);

                            List<string> addClaims = userGroupTokens.Except(claimList).ToList();
                            List<string> deleteClaims = claimList.Except(userGroupTokens).ToList();

                            foreach (var claimName in addClaims)
                            {
                                var claim = new Claim("UserRight", claimName);
                                await userManager.AddClaimAsync(customUser.Id, claim);
                            }

                            foreach (var claimName in deleteClaims)
                            {
                                var claim = new Claim("UserRight", claimName);
                                await userManager.RemoveClaimAsync(customUser.Id, claim);
                            }

                            #endregion

                            HttpContext.GetOwinContext().Authentication.SignIn(
                                new AuthenticationProperties() { IsPersistent = true },
                                ident);

                            return RedirectToLocal("/");
                        }

                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = "/", RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            else
                return View("Index",model);

            return Json(new { isLogin = isLogin, message = message, url = url, strUserHashKey = strUserHashKey, userName = model.UserName });
        }

        public string getloginFailureReasonMessage(User user)
        {
            string errorMessage = string.Empty;

            if (user == null)
            {
                errorMessage = "نام کاربری یا رمز عبور صحیح نمی باشد";
            }
            else if (!user.IsVerified || !user.VerificationDateTime.HasValue)
            {
                errorMessage = "حساب کاربری کاربر فعال نشده است";
            }
            else if (!user.IsActivated)
            {
                errorMessage = "کاربر غیر فعال است";
            }
            return errorMessage;
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// remeber me
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="strUserHashKey"></param>
        /// <returns></returns>
        public ActionResult CheckHashKey(string userName, string strUserHashKey)
        {
            bool canLogin = false;
            var url = Url.Action("Login", "Account");
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(strUserHashKey))
            {
                try
                {
                    var customUser = signInManager.UserManager.FindByName(userName);

                    var user = userBiz.FirstOrDefault(x => x.Id == customUser.User_Id);

                    if (user != null)
                    {
                        var key = Convert.FromBase64String(strUserHashKey);
                        var userHashKey = new Guid(key);

                        if (userHashKey == user.UserHashKey)
                        {
                            url = Url.Action("Index", "Home");
                            canLogin = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { url = url }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { url = url, canLogin = canLogin });
        }
        #endregion

        #region [ثبت نام کاربر]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(RegisterViewModel model)
        {
            bool isRegister = false;
            var url = string.Empty;

            if (ModelState.IsValid)
            {

                #region [check MobileNumber is duplicate]

                var duplicateUser = userBiz.FetchMulti(x => x.MobileNumber == model.MobileNumber);

                if (duplicateUser != null && duplicateUser.Any())
                    throw new Exception("این شماره موبایل قبلا ثبت شده است");

                #endregion

                #region [insert in User table]
                var user = new Common.Models.User()
                {
                    UserGroup_Id = (int)UserGroupEnum.User,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreationDateTime = DateTime.Now,
                    Reputation = 0,
                    LockedCredit = 0,
                    IsActivated = false,
                    MobileNumber = model.MobileNumber,
                    ActivationCode = DateTime.Now.Ticks.ToString().GetHashCode().ToString("x"),
                    IsVerified = false
                };

                userBiz.Create(user);
                #endregion

                #region [create user and insert row in AspNetUsers tables]
                var customUser = new CustomUser() { UserName = model.Email, Email = model.Email, User_Id = user.Id };

                var result = await signInManager.UserManager.CreateAsync(customUser, model.Password);
                #endregion

                #region [create user successfully]
                if (result.Succeeded)
                {
                    #region [add claims to new user]

                    await AddClaims(user.UserGroup_Id, customUser.Id);

                    #endregion

                   

                    isRegister = true;
                    url = Url.Action("Login", "Account");

                    #region [send activation code by sms]
                    //userBiz.SendActivationCode(user);
                    #endregion

                }
                #endregion
                #region [create user failed]
                else
                {
                    userBiz.Delete(user);

                    var errors = new List<string>();
                    errors.AddRange(result.Errors.ToList());

                    //CustomException.Throw(errors);
                }
                #endregion
            }

            return Json(new { isRegister = isRegister, url = url });
        }
        #endregion

        #region [تغییر رمز عبور توسط کاربر]
        [ClaimBasedAuthorzation(TokenName = "نمایش فرم تغییر رمز عبور توسط کاربر", ClaimType = "UserRight")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [ClaimBasedAuthorzation(TokenName = "تغییر رمز عبور توسط کاربر", ClaimType = "UserRight")]
        public async Task<ActionResult> UserChangePassword(ChangePasswordViewModel model)
        {
            var message = "امکان تغییر رمز عبور وجود ندارد";
            var isChange = false;
            var url = "";
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                if (model.UserName != userName)
                {
                    message = "نام کاربری نادرست است";
                    return Json(new { message = message, isChange = isChange });
                }

                var customUser = signInManager.UserManager.Find(model.UserName, model.PreviusPassword);

                if (customUser == null)
                {
                    message = "نام کاربری یا رمز عبور نادرست است";
                    return Json(new { message = message, isChange = isChange });
                }

                await ResetPassword(model.NewPassword, model.UserName);

                isChange = true;
                message = "رمز عبور تغییر کرد ";

                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                url = Url.Action("Login", "Account");
            }

            return Json(new { message = message, isChange = isChange, url = url });
        }
        #endregion

        #region [فراموشی رمز عبور]
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> ResetPasswordAndSendMessage(string userName)
        {
            var isChange = false;
            var message = "";
            var url = "";

            var customUser = await signInManager.UserManager.FindByNameAsync(userName);

            if (customUser == null)
            {
                message = "نام کاربری نادرست است";
                return Json(new { message = message, isChange = isChange });
            }

            var newPassword = GenerateNewPassword();
            await ResetPassword(newPassword, userName);

            isChange = true;
            message = "بازیابی رمز عبور با موفقیت انجام شد.یک ایمیل حاوی رمز عبور برای شما ارسال خواهد شد";
            url = Url.Action("Login", "Account");
            return Json(new { message = message, isChange = isChange, url = url });
        }
        #endregion


         
        public bool IsValidCaptcha(string response)
        {
            //var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "YOUR KEY VALUE PAIR";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return true;

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ViewBag.Message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        ViewBag.Message = "The secret parameter is invalid or malformed.";
                        break;

                    case ("missing-input-response"):
                        ViewBag.Message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        ViewBag.Message = "The response parameter is invalid or malformed.";
                        break;

                    default:
                        ViewBag.Message = "Error occured. Please try again";
                        break;
                }
                return false;
            }
            return true;
        }


        #region [Captcha]
        public ActionResult CaptchaView()
        {

            var model = new CaptchaClass();
            SetCaptcha(ref model);
            return View(model);
        }

        public ActionResult SubmitCaptcha(CaptchaClass model)
        {
            var message = string.Empty;

            //if (!ValidateCaptcha(model)
            //    View(model);
            //model.Message = "کلمه امنیتی درست است";
            //return View(model);

            ValidateCaptcha(model, ref message);
            return Json(message);
        }

        public ActionResult RefreshCaptcha()
        {
            var model = new CaptchaClass();
            SetCaptcha(ref model);
            return Json(model);
        }

        private void SetCaptcha(ref CaptchaClass model)
        {

            model.CaptchaText = string.Empty;

            var captchaResult = string.Empty;
            var CaptchaText = GenerateCaptchaText(ref captchaResult);


            var CaptchaImageUrl = string.Format(@"http://localhost:9922/Home/ShowImage?text={0}", CaptchaText);

            model.CaptchaImageUrl = CaptchaImageUrl;
            model.CaptchaResult = captchaResult;

        }

        private string GetCaptcha(CaptchaClass model)
        {
            //var farsiAlphabatic = NumberToString.ConvertIntNumberToFarsiAlphabatic(model.CaptchaText);
            var encryptedString =
                 HttpUtility
                 .UrlEncode(
                     Encryptor.Encrypt(
                         model.CaptchaText
                     )
                 );

            return encryptedString;
        }

        private bool ValidateCaptcha(CaptchaClass model, ref string message)
        {
            if (!Utils.IsNumber(model.CaptchaText))
            {
                message = "تصویر امنیتی را بطور صحیح وارد نکرده اید";
                return false;
            }

            var strGetCaptcha =
                GetCaptcha(model)
                ;

            //var strDecodedVAlue =
            //    model.CaptchaResult
            //    ;

            var strDecodedVAlue =
                HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                       model.CaptchaResult
                    )
                );

            if (strDecodedVAlue != strGetCaptcha)
            {
                message = "کلمه امنیتی اشتباه است";
                SetCaptcha(ref model);
                return false;
            }
            message = "کلمه امنیتی درست است";

            return true;
        }

        public ActionResult ShowImage()
        {
            if (HttpContext.Request.Params["text"] == null) return null;

            const int heightTotalImage = 25;
            const int widthTotalImage = 155;

            var queryStringValue = HttpContext.Request.Params["text"];

            var sImageText =
                Encryptor.Decrypt(queryStringValue);

            var objBmpImage = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

            // Create the Font object for the image text drawing.
            var objFont = new Font("Tahoma", 9, FontStyle.Regular, GraphicsUnit.Point);

            // Create a graphics object to measure the text's width and height.
            var objGraphics = Graphics.FromImage(objBmpImage);


            // Thies is where the bitmap size is determined.
            var intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width + 10;
            var intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height + 10;

            var floatX = (widthTotalImage - intWidth) / 2;
            var floatY = (heightTotalImage - intHeight) / 2;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(widthTotalImage, heightTotalImage));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.HighQuality;
            objGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.Black), floatX, floatY);

            // Adds a simple wave
            double distort = RandomGenerator.Next(2, 5) * (RandomGenerator.Next(5) == 1 ? 1 : -1);
            using (var copy = (Bitmap)objBmpImage.Clone())
            {
                for (var y = 0; y < heightTotalImage; y++)
                {
                    for (var x = 0; x < widthTotalImage; x++)
                    {
                        var newX = (int)(x + (distort * Math.Sin(Math.PI * y / 84.0)));
                        var newY = (int)(y + (distort * Math.Cos(Math.PI * x / 44.0)));

                        if (newX < 0 || newX >= widthTotalImage) newX = 0;
                        if (newY < 0 || newY >= heightTotalImage) newY = 0;

                        var dd = copy.GetPixel(newX, newY);
                        objBmpImage.SetPixel(x, y, dd);
                    }
                }
            }

            HttpContext.Response.ContentType = "image/jpeg";
            objBmpImage.Save(HttpContext.Response.OutputStream, ImageFormat.Jpeg);
            return null;
        }

        public static string GenerateCaptchaText(ref string captchaResult)
        {
            var number1 =
                RandomGenerator.Next(1, 99)
                ;

            var number2 =
               RandomGenerator.Next(1, 99)
               ;

            // var farsiAlphabatic = number1 + "+" + number2;

            captchaResult = (number1 + number2).ToString();

            var captchaText =
                HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                        number1 + "+" + number2
                    )
                );

            return captchaText;
        }
        public class CaptchaClass
        {
            public string CaptchaText { get; set; }
            public string CaptchaImageUrl { get; set; }
            public string CaptchaResult { get; set; }
        }

        #endregion


        [HttpPost]
        public ActionResult ValidateCaptcha()
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "YOUR KEY VALUE PAIR";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return View();

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ViewBag.Message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        ViewBag.Message = "The secret parameter is invalid or malformed.";
                        break;

                    case ("missing-input-response"):
                        ViewBag.Message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        ViewBag.Message = "The response parameter is invalid or malformed.";
                        break;

                    default:
                        ViewBag.Message = "Error occured. Please try again";
                        break;
                }
            }
            else
            {
                ViewBag.Message = "Valid";
            }

            return View("Login");
        }


        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}