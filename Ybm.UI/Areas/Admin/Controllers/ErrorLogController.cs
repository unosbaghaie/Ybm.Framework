using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Ybm.Common.Models;
using Ybm.Framework;

namespace Ybm.UI.Areas.Admin.Controllers
{
    public class ErrorLogController : Controller
    {
        // GET: Admin/ErrorLog
        public ActionResult Index()
        {
            return View();
        }




        #region [IFilterable Functions]
        public List<CustomFilterDescriptor> FilterData
        {
            get
            {
                ErrorLog q = new ErrorLog();
                List<CustomFilterDescriptor> decriptors = Framework.ExpressionHelper.
                    ExpressionBuilder.GetFilterFields<ErrorLog>(
                    new Tuple<Expression<Func<object>>, string>(() => q.Id, "شناسه"),
                    new Tuple<Expression<Func<object>>, string>(() => q.CreationDateTime, "تاریخ ثبت"),
                    new Tuple<Expression<Func<object>>, string>(() => q.Message, "توضیحات")
                    );

                return decriptors;


            }
            set { }
        }
        public ActionResult FilterThePage(Dictionary<string, string> selectedFilters)
        {
            var filterData = FilterData;
            filterData.ForEach(q => q.Member = q.Member.Replace("FilterField_", ""));
            Framework.ExpressionHelper.ExpressionBuilder.SyncFilterData(filterData, selectedFilters);
            var predicate = Framework.ExpressionHelper.ExpressionBuilder.MakeTheExpression<ErrorLog>(filterData);
            TempData["predicate"] = predicate;
            return Json(new { result = true });
        }


        #endregion
    }
}