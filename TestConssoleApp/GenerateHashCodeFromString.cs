using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ybm.Framework.Extension;

namespace TestConssoleApp
{
    public class GenerateHashCodeFromString
    {

        public void Run()
        {

            List<string> entities = new List<string>();
            entities.Add("AspNetRoleAspNetRoleAspNetRoleAspNetRole");
            entities.Add("AspNetUserClaimAspNetUserClaimAspNetUserClaimAspNetUserClaim");
            entities.Add("AspNetUserLoginAspNetUserLoginAspNetUserLoginAspNetUserLogin");
            entities.Add("AspNetUserAspNetUserAspNetUserAspNetUser");
            entities.Add("ErrorLogErrorLogErrorLogErrorLog");
            entities.Add("ErrorTypeErrorTypeErrorTypeErrorTypeErrorTypeErrorTypeErrorTypeErrorTypeErrorType");



            Dictionary<string, string> dict1 = new Dictionary<string, string>();

            foreach (var item in entities)
            {
                dict1.Add(item, item.GetStableHashCode().ToString());
            }

            Dictionary<string, string> dict2 = new Dictionary<string, string>();

            foreach (var item in entities)
            {
                dict2.Add(item, item.GetStableHashCode().ToString());
            }






            foreach (var item in dict1)
            {
                if (dict2[item.Key] == item.Value)
                    Console.WriteLine("eq");
            }

        }
    }
}
