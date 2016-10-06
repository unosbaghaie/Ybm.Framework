using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Engine
{
    public class SubscribeSchema
    {
        public int Id { get; set; }
        public Type SubscriberType { get; set; }
        public MethodInfo SubscriberMethod { get; set; }
        public string SubscribeEventName { get; set; }
        public Type SubscribeEventEnclosingType { get; set; }

        private static Dictionary<string, List<SubscribeSchema>> SubscribeDictionary = new Dictionary<string, List<SubscribeSchema>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFullName"> Type name which is going to be instanciated . for example IBankBusiness 
        /// is going to be instanciated, therefore all the SubsribeToes to BankBusiness will be found and would be registered to BankBusiness events</param>
        /// <returns></returns>
        public static List<SubscribeSchema> Get(string typeFullName)
        {
            var item = SubscribeDictionary.FirstOrDefault(q => q.Key == typeFullName);
            if (item.Equals(new KeyValuePair<string, string>()))
                return null;
            return item.Value;
        }

        public static SubscribeSchema Add(string typeFullName,SubscribeSchema model)
        {
            List<SubscribeSchema> list;
            if (SubscribeDictionary.TryGetValue(typeFullName, out list))
            {
                var existedModel = list.FirstOrDefault(q => q.SubscribeEventName == model.SubscribeEventName && q.SubscriberMethod == model.SubscriberMethod);
                if (existedModel == null)
                {
                    list.Add(model);
                }
                else
                {
                    existedModel = model;
                }
                return model;
            }
            else
            {
                SubscribeDictionary.Add(typeFullName, new List<SubscribeSchema>() { model });
                return model;
            }
            return null;
        }

    }
}
