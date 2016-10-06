using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework
{
    public class CustomFilterDescriptor
    {

        public object ConvertedValue { get; set; }
        public string Member { get; set; }
        public Type MemberType { get; set; }
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }

        public string Name { get; set; }
    }
}
