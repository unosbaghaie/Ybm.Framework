using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class Page
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string PageHref { get; set; }
        public bool IsVisible { get; set; }
        public Nullable<int> Parent_Id { get; set; }
        public int UserGroup_Id { get; set; }
        public System.DateTime CreationDateTime { get; set; }
        public int Priority { get; set; }
        public string SelectorCssClass { get; set; }
        public int PageScope_Id { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
