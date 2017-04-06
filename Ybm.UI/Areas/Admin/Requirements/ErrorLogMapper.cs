using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ybm.Common.Models;
using Ybm.UI.Areas.Admin.Requirements.ViewModels;

namespace Ybm.UI.Areas.Admin.Requirements
{
    public static class ErrorLogMapper
    {

        public static ErrorLogViewModel GetViewModel(this ErrorLog model)
        {








            return new ErrorLogViewModel();
        }


    }
}