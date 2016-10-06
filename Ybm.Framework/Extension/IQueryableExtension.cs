using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Extension
{
    public static class IQueryableExtension
    {
        public static string GetRawQuery(this IQueryable query)
        {
            return query.ToString();
        }
    }
}
