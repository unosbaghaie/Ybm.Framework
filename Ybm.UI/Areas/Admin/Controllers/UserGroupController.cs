using Kendo.Mvc.UI;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Ybm.Business;
using Ybm.Common.Models;
using Ybm.Framework;
using Ybm.Framework.ExpressionHelper;
using Ybm.UI.Areas.Admin.Requirements;

namespace Ybm.UI.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {
        // GET: Admin/UserGroup
        IUserGroupBusiness userGroupBiz = ServiceFactory.CreateInstance<IUserGroupBusiness>();



        // GET: Admin/ErrorLog
        public ActionResult Index()
        {
            #region [IFilterable View Data]
            ViewBag.filterControlsModel = FilterData;
            //ViewBag.User_Id = User_Id;
            #endregion

            return View();
        }


        #region [IFilterable Functions]
        public List<CustomFilterDescriptor> FilterData
        {
            get
            {
                UserGroup q = new UserGroup();
                List<CustomFilterDescriptor> decriptors = Framework.ExpressionHelper.
                    ExpressionBuilder.GetFilterFields<ErrorLog>(
                    new Tuple<Expression<Func<object>>, string>(() => q.Id, "شناسه"),
                    new Tuple<Expression<Func<object>>, string>(() => q.Name, "نام")
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


        //[ClaimBasedAuthorzation(TokenName = "پر کردن گیرید شکایات کاربر", ClaimType = "UserRight")]
        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request, byte? status = null)
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            Expression<Func<UserGroup, bool>> basePredicate = null;

            #region [IFilterable Predicate]
            if (TempData["predicate"] != null)
                basePredicate = basePredicate.And((Expression<Func<UserGroup, bool>>)TempData["predicate"]);
            #endregion

            IPagedList<UserGroup> list;
            list = userGroupBiz.FetchMulti(
                request.Page,
                request.PageSize,
                q => q.Id, basePredicate,
                false);

            var count = userGroupBiz.Count(basePredicate);
            return Json(list.ToList().Select(q => q.GetViewModel()).ToDataSourceResult(request, count), JsonRequestBehavior.AllowGet);
        }
    }
}