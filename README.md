Welcome to the Ybm.Framework description

It is a framework to facilitate writing web based applications

the first version is done at 2016/10/06 
features:

_ Instanciate Business layer classese in UI or Business itself

        IUserBusiness userBiz = ServiceFactory.CreateInstance<IUserBusiness>();

_ Tansactional methods in Business Layer

        [Transactional]
        public void CreateUser()
        {
            // fetch a record 
            // create an entity
            // update an entity 
            // and so on 
        }

_ Subsribe and publish 

        [SubscribeTo(typeof(IUserBusiness),"AnEventToSubscribeToInIUserBusiness")]
        //this method would be invoked when the AnEventToSubscribeToInIUserBusiness event is invoked
        public void CheckIfUserIsActive()
        {

        }
        
        
you can override some methods in business layer to handle Create, Upate and delete entities

        public override void OnBeforeSavingRecord(object sender, EntitySavingEventArgs<User> e)
        {
            base.OnBeforeSavingRecord(sender, e);
        }
        public override void OnSavingRecord(object sender, EntitySavingEventArgs<User> e)
        {
            base.OnSavingRecord(sender, e);
        }
        public override void OnRecordSaved(object sender, EntitySavingEventArgs<User> e)
        {
            base.OnRecordSaved(sender, e);
        }
        public override void OnUpdatingRecord(object sender, EntitySavingEventArgs<User> e)
        {
            base.OnUpdatingRecord(sender, e);
        }
        public override void OnRecordUpdated(object sender, EntitySavingEventArgs<User> e)
        {
            base.OnRecordUpdated(sender, e);
        }

        public override void OnBeforeDeletingRecord(object sender, EntityDeletingEventArgs<User> e)
        {
            base.OnBeforeDeletingRecord(sender, e);
        }
        public override void OnDeletingRecord(object sender, EntityDeletingEventArgs<User> e)
        {
            base.OnDeletingRecord(sender, e);
        }
        public override void OnRecordDeleted(object sender, EntityDeletingEventArgs<User> e)
        {
            base.OnRecordDeleted(sender, e);
        }
        

ClaimBasedAuthorzation on controllers:
which gather all Claims information and store in Tokens table to Authorize
a page to manage userGroups' Tokens is provided

    [TokenCategory(CategoryTitle = "Home Page", CategoryName = "HomePage")]
    public class HomeController : Controller
    {
        [ClaimBasedAuthorzation(TokenName = "View Home Page", ClaimType = "UserRight")]
        public ActionResult Index()
        {
            return View();
        }

        [ClaimBasedAuthorzation(TokenName = "Do Something", ClaimType = "UserRight")]
        public ActionResult DoSomething()
        {
            return View();
        }
    }
