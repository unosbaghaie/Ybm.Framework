using Ybm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Infrastructure.Authorization
{
    public class TokenCategoryAttribute : Attribute
    {
        public string CategoryName { get; set; }
        public string CategoryTitle { get; set; }
        public TokenCategoryAttribute()
        {
                
        }
    }
}