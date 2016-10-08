using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class Token
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string PersianName { get; set; }
        public Nullable<int> TokenCategory_Id { get; set; }
    }
}
