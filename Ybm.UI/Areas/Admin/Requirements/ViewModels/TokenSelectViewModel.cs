using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Areas.Admin.Requirements.ViewModels
{
    public class TokenSelectViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTitle { get; set; }
        public string AreaName { get; set; }
        public bool Checked { get; set; }
    }
}