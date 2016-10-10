using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.UI.Infrastructure.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }

        public string UserName { get; set; }

        public string PreviusPassword { get; set; }
    }
}
