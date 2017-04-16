using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Areas.Admin.Requirements.ViewModels
{
    public class UserGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

    }
}