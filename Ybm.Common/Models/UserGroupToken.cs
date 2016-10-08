using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class UserGroupToken
    {
        public int Id { get; set; }
        public int UserGroup_Id { get; set; }
        public int Token_Id { get; set; }
    }
}
