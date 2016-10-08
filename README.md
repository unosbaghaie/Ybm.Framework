Welcome to the Ybm.Framework wiki!

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
        
        
        
