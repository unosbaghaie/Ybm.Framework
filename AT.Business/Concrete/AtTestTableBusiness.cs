using AT.Business.Interface;
using AT.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ybm.Framework.Service;

namespace AT.Business.Concrete
{
        public partial class AtTestTableBusiness : Service<AtTestTable>, IAtTestTableBusiness
    {
            public AtTestTableBusiness()
                //: base(ContainerManager.Container.Resolve<YbmContext>())
            {
                base.PopulateEvents(this);
            }
        }
}
