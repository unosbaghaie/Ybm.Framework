using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class User
    {
        public User()
        {
            this.AspNetUsers = new List<AspNetUser>();
        }

        public int Id { get; set; }
        public int UserGroup_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime CreationDateTime { get; set; }
        public int Reputation { get; set; }
        public decimal LockedCredit { get; set; }
        public bool IsActivated { get; set; }
        public string MobileNumber { get; set; }
        public string ActivationCode { get; set; }
        public Nullable<System.Guid> UserHashKey { get; set; }
        public bool IsVerified { get; set; }
        public Nullable<System.DateTime> VerificationDateTime { get; set; }
        public Nullable<bool> IsFirstLogin { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
