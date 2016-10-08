using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public bool IsCustomError { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string IpAddress { get; set; }
        public System.DateTime CreationDateTime { get; set; }
        public byte ErrorType_Id { get; set; }
        public Nullable<int> User_Id { get; set; }
    }
}
