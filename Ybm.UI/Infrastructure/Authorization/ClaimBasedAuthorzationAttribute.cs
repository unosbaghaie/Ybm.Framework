using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Ybm.UI.Infrastructure.Authorization
{
    public class ClaimBasedAuthorzationAttribute : AuthorizeAttribute
    {

        public string CategoryName { get; set; }
        public string AreaName { get; set; }

        // in the real world you could get claim value form the DB, 
        // I simplified the example 
        public string TokenName { get; set; }
        public string ClaimType { get; set; }
        public string Value { get; set; }

        protected override bool AuthorizeCore(HttpContextBase context)
        {
           
            var routeData = context.Request.RequestContext.RouteData;
            var currentController = routeData.GetRequiredString("controller"); //Example
            var currentAction = routeData.GetRequiredString("action"); //Index

            //var fullName = CategoryName +"_"+ currentController + "Controller_" + currentAction;
            var fullName = AreaName + CategoryName + "_" + currentAction;
            return context.User.Identity.IsAuthenticated;
                //&& context.User.Identity is ClaimsIdentity
                //&& ((ClaimsIdentity)context.User.Identity).HasClaim(x =>
                //    x.Type == ClaimType && x.Value.ToLower() == fullName.ToLower());
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controllerType = filterContext.Controller.GetType();
            var tokenCategoryAttr = controllerType.GetCustomAttributes(typeof(TokenCategoryAttribute), false);
            if (tokenCategoryAttr != null)
            {
                var tokenAttr = (TokenCategoryAttribute)tokenCategoryAttr[0];
                var categoryName = tokenAttr.CategoryName;
                var categoryTitle = tokenAttr.CategoryTitle;
                CategoryName = categoryName;
            }

            if (!string.IsNullOrEmpty(controllerType.Namespace) && (controllerType.Namespace.Count(q => q == '.') > 2))
                AreaName = controllerType.Namespace.Split('.')[3] + "_";

            //if (filterContext.Controller.ToString().Contains("Areas"))
            //    controllerFullName = filterContext.Controller.ToString();
            //else
            //    controllerFullName = null;
            base.OnAuthorization(filterContext);
        }


    }
}