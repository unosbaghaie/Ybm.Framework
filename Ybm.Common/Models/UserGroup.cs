using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class UserGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
