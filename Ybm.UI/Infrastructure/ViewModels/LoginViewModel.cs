using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Infrastructure.ViewModels
{
    public class LoginViewModel
    {

        //[Required(ErrorMessage = "نام کاربری را وارد نمایید")]
        //[Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        //[Display(Name = "رمز عبور")]
        public string Password { get; set; }
        //[Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
        public string UserHashKey { get; set; }
    }
}