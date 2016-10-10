using System;
using System.Collections.Generic;

namespace Ybm.Common.Models
{
    public partial class TokenCategory
    {
        public TokenCategory()
        {
            this.Tokens = new List<Token>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PersianName { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
