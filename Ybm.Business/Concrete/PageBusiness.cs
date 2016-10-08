
using Ybm.Common.Models;
using Ybm.Framework;
using Ybm.Framework.Attribute;
using Ybm.Framework.Service;

namespace Ybm.Business
{
    public partial class PageBusiness : Service<Page>, IPageBusiness
    {
        public PageBusiness()
            :base(ContainerManager.Container.Resolve<YbmContext>())
        {
                base.PopulateEvents(this);
        }
    }
}