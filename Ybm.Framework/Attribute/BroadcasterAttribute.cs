using Ybm.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Attribute
{
   

    [AttributeUsage(AttributeTargets.Method)]
    public class BroadcasterAttribute : System.Attribute
    {
       

        public BroadcasterAttribute(Type Type,string MethodName, bool RunFirst,params object[] parameters)
        {

            

        }
        public Type Type { get; set; }
        public string MethodName{ get; set; }




    }

    public enum TextEnum
    {
        test1,
        test2
    }

}
