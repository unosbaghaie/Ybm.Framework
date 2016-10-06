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

namespace Cms.Business
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

        }


        
    }
}