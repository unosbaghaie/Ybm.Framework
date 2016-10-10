using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            this.Pages = new List<Page>();
            this.UserGroupTokens = new List<UserGroupToken>();
            this.Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
        public virtual ICollection<UserGroupToken> UserGroupTokens { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
