
using Ybm.Common.Models;
using Ybm.Framework;
using Ybm.Framework.Attribute;
using Ybm.Framework.Service;

namespace Ybm.Business
{
    public partial class ErrorTypeBusiness : Service<ErrorType>, IErrorTypeBusiness
    {
        public ErrorTypeBusiness()
            :base(ContainerManager.Container.Resolve<YbmContext>())
        {
                base.PopulateEvents(this);
        }
    }
}