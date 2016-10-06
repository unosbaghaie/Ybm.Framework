using Ybm.Common;
using Ybm.Common.Models;
using Ybm.Framework;
using Ybm.Framework.Attribute;
using Ybm.Framework.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ybm.Business;
using Ybm.Framework.Eventing;

namespace Ybm.Business
{
    public partial class UserBusiness : Service<User>, IUserBusiness
    {
      
        public UserBusiness()
            : base(ContainerManager.Container.Resolve<YbmContext>())
        {

        }


        [Transactional]
        public void CreateUser()
        {
            IUserBusiness userNiz = ServiceFactory.CreateInstance<IUserBusiness>();
            // fetch a record 

            // create an entity

            // update an entity 

            // and so on 
        }

        

        [SubscribeTo(typeof(IUserBusiness),"AnEventToSubscribeToInIUserBusiness")]
        //this method would be invoked when the AnEventToSubscribeToInIUserBusiness event is invoked
        public void CheckIfUserIsActive()
        {

        }






    }
}