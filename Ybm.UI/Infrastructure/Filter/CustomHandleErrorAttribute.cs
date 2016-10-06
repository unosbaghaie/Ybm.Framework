using Ybm.Business;
using Ybm.Common;
using Ybm.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ybm.UI.Infrastructure.Filter
{
    public class CustomHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        //todo:dbContext will be disposed and insert in errorLog table failed
        public void OnException(ExceptionContext filterContext)
        {
            //IErrorLogBusiness errorLogBiz = ServiceFactory.CreateInstance<IErrorLogBusiness>("IErrorLogBusinessThread");
            //int? userId = null;

            //if (HttpContext.Current.Session["UserId"] != null)
            //    userId = (int)HttpContext.Current.Session["UserId"];

            //List<string> errors = new List<string>();

            //if (filterContext.Exception is CustomException)
            //{
            //    var customException = filterContext.Exception as CustomException;
            //    if (customException.Messages != null && customException.Messages.Any())
            //    {
            //        errors.AddRange(customException.Messages);
            //    }
            //    //errorLogBiz.SaveCustomException(customException, EnumErrorType.General, userId);

            //}
            //else
            //{
            //    errors.Add(filterContext.Exception.Message);


            //    errorLogBiz.SaveSystemError(filterContext.Exception, HttpContext.Current.Request.UserHostAddress, EnumErrorType.General, userId);
            //    ServiceFactory.Release(errorLogBiz);
            //}

            //filterContext.Result = new JsonResult()
            //{
            //    Data = new
            //    {
            //        key = "CustomException",
            //        errors = errors
            //    }
            //};


            //filterContext.ExceptionHandled = true;
            //filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; // Consider using more error status codes depending on the type of exception
            //filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}