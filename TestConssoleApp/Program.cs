using AT.Common.Models;
using AT.Common.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ybm.Common.Models;
using Ybm.Common.Models.Mapping;
using Ybm.Framework.Context;

namespace TestConssoleApp
{
    class Program
    {
        static void Main(string[] args)
        {


            List<string> entities = new List<string>();
            entities.Add("AspNetRole");
            entities.Add("AspNetUserClaim");
            entities.Add("AspNetUserLogin");
            entities.Add("AspNetUser");
            entities.Add("ErrorLog");
            entities.Add("ErrorType");
            entities.Add("Page");
            entities.Add("TokenCategory");
            entities.Add("Token");
            entities.Add("UserGroup");
            entities.Add("UserGroupToken");
            entities.Add("User");
            entities.Add("AtTestTable");


            List<Type> types = new List<Type>();
            types.Add(typeof(AtTestTable));
            types.Add(typeof(AtTestTableMap));
            types.Add(typeof(User));
            types.Add(typeof(UserMap));


            new ContextGenerator().Generate(entities, types.ToArray());



        }
    }
}
