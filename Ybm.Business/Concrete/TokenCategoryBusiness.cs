
using Ybm.Common.Models;
using Ybm.Framework;
using Ybm.Framework.Attribute;
using Ybm.Framework.Service;

namespace Ybm.Business
{
    public partial class TokenCategoryBusiness : Service<TokenCategory>, ITokenCategoryBusiness
    {
        public TokenCategoryBusiness()
            :base(ContainerManager.Container.Resolve<YbmContext>())
        {
                base.PopulateEvents(this);
        }
    }
}