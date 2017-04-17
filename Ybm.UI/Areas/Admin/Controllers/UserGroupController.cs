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
using Ybm.UI.Areas.Admin.Requirements.ViewModels;

namespace Ybm.UI.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {
        // GET: Admin/UserGroup
        IUserGroupBusiness userGroupBiz = ServiceFactory.CreateInstance<IUserGroupBusiness>();
        IUserBusiness userBiz = ServiceFactory.CreateInstance<IUserBusiness>();



        // GET: Admin/UserGroup
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
                List<CustomFilterDescriptor> decriptors = Framework.ExpressionHelper.
                    ExpressionBuilder.GetFilterFields2(
                    new MetaData<UserGroupTestViewModel>(a => a.Id, "شناسه"),
                    new MetaData<UserGroupTestViewModel>(a => a.FirstName, "شناسه"),
                    new MetaData<UserGroupTestViewModel>(a => a.LastName, "شناسه")
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
            var predicate = Framework.ExpressionHelper.ExpressionBuilder.MakeTheExpression<UserGroupTestViewModel>(filterData);
            TempData["predicate"] = predicate;
            return Json(new { result = true });
        }


        #endregion


        //[ClaimBasedAuthorzation(TokenName = "پر کردن گیرید شکایات کاربر", ClaimType = "UserRight")]
        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request, byte? status = null)
        {

            var query = from ug in userGroupBiz.FetchMulti()
                        join u in userBiz.FetchMulti() on ug.Id equals u.UserGroup_Id
                        orderby u.Id
                        select new UserGroupTestViewModel()
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            GroupName = ug.Name,
                        };

            #region [IFilterable Predicate]

            var lastMonth = DateTime.Now.AddMonths(-1);
            Expression<Func<UserGroupTestViewModel, bool>> basePredicate = null;
            
            if (TempData["predicate"] != null)
                basePredicate = basePredicate.And((Expression<Func<UserGroupTestViewModel, bool>>)TempData["predicate"]);

            query = basePredicate == null ? query : query.Where(basePredicate);

            #endregion

            var items = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
            var totalItemCount = query.Count();
            IPagedList<UserGroupTestViewModel> list =  new StaticPagedList<UserGroupTestViewModel>(items, request.Page, request.PageSize, totalItemCount);
            
            return Json(list.ToList().ToDataSourceResult(request, totalItemCount), JsonRequestBehavior.AllowGet);
        }
    }
}