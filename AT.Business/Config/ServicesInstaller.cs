using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ybm.Business;
using Ybm.Common.Models;
using Ybm.Framework.Aop;

namespace Ybm.Framework
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<YbmContext>().ImplementedBy<YbmContext>().Named("YbmContextThread").LifestylePerThread());
            //container.Register(Component.For<YbmContext>().ImplementedBy<YbmContext>().LifestylePerWebRequest());
            
            ////container.Register(Component.For<IErrorLogBusiness>().ImplementedBy<ErrorLogBusiness>().Named("IErrorLogBusinessThread").LifestylePerThread());

            //container.Register(Component.For<TransactionalInterceptor>().LifestylePerWebRequest());
            //container.Register(Component.For<BroadcasterInterceptor>().LifestylePerWebRequest());

            

            //container.Register(Component.For<IUserBusiness>().ImplementedBy<UserBusiness>().LifestylePerWebRequest());
            //container.Register(Component.For<IPageBusiness>().ImplementedBy<PageBusiness>().LifestylePerWebRequest());
            //container.Register(Component.For<ITokenCategoryBusiness>().ImplementedBy<TokenCategoryBusiness>().LifestylePerWebRequest());
            //container.Register(Component.For<IUserGroupTokenBusiness>().ImplementedBy<UserGroupTokenBusiness>().LifestylePerWebRequest());
            //container.Register(Component.For<ITokenBusiness>().ImplementedBy<TokenBusiness>().LifestylePerWebRequest());
            //container.Register(Component.For<IUserGroupBusiness>().ImplementedBy<UserGroupBusiness>().LifestylePerWebRequest());
            container.Register(Component.For<IAtTestTableBusiness>().ImplementedBy<ErrorLogBusiness>().LifestylePerWebRequest());

        }
    }
}
