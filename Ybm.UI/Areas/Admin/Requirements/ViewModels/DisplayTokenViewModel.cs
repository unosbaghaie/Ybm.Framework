using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Areas.Admin.Requirements.ViewModels
{
    public class DisplayTokenViewModel
    {
        public int UserGroupId { get; set; }
        public string AreaName { get; set; }
        public List<TokenSelectViewModel> TokenSelectViewModel { get; set; }
    }

}