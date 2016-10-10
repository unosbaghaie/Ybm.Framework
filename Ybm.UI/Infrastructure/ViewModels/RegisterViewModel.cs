using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.UI.Infrastructure.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "نام را وارد نمایید")]
        //[Display(Name = "نام")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "نام خانوادگی را وارد نمایید")]
        //[Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "شماره همراه را وارد نمایید")]
        //[Display(Name = "شماره همراه")]
        public string MobileNumber { get; set; }

        //[Required(ErrorMessage = "ایمیل را وارد نمایید")]
        //[EmailAddress(ErrorMessage = "ایمیل وارد شده نامعتبر است")]
        //[Display(Name = "ایمیل")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        //[Display(Name = "رمز عبور")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "تایید رمز عبور را وارد نمایید")]
        //[Display(Name = "تایید رمز عبور")]
        //[Compare("Password", ErrorMessage = "تایید رمز عبور با رمز عبور برابر نیست")]
        public string ConfirmPassword { get; set; }
    }
}
