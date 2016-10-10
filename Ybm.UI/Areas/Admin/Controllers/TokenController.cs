using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ybm.Business;
using Ybm.Framework;
using Ybm.UI.Areas.Admin.Requirements.ViewModels;
using Ybm.UI.Infrastructure;
using Ybm.UI.Infrastructure.Authorization;

namespace Ybm.UI.Areas.Admin.Controllers
{
    [TokenCategory(CategoryTitle = "صفحه مجوزهای دسترسی", CategoryName = "TokenPage")]
    public class TokenController : MvcController
    {

        ITokenBusiness tokenBiz = ServiceFactory.CreateInstance<ITokenBusiness>();
        ITokenCategoryBusiness tokenCategoryBiz = ServiceFactory.CreateInstance<ITokenCategoryBusiness>();
        IUserGroupBusiness userGroupBiz = ServiceFactory.CreateInstance<IUserGroupBusiness>();
        IUserGroupTokenBusiness userGroupTokenBiz = ServiceFactory.CreateInstance<IUserGroupTokenBusiness>();
        List<TokenSelectViewModel> list = new List<TokenSelectViewModel>();


        [ClaimBasedAuthorzation(TokenName = "نمایش فرم مجوزهای دسترسی", ClaimType = "UserRight")]
        public ActionResult Index()
        {
            var permissionList = getAuthorizedActions();

            var areas = (from p in permissionList
                         group p by p.AreaName into g
                         select new
                         {
                             g.Key
                         }).ToList();

            var modelList = new List<DisplayTokenViewModel>();
            foreach (var area in areas)
            {
                var model = new DisplayTokenViewModel()
                {
                    TokenSelectViewModel = permissionList.Where(q => q.AreaName == area.Key).ToList(),
                    AreaName = string.IsNullOrWhiteSpace(area.Key) ? "Common" : area.Key,
                    UserGroupId = 0
                };
                modelList.Add(model);
            }

            return View(modelList);
        }

        [ClaimBasedAuthorzation(TokenName = "افزودن مجوزهای دسترسی جدید", ClaimType = "UserRight")]
        public ActionResult Create(string[] tokenIds, int userGroupId)
        {
            var selectedIds = tokenBiz.FetchMulti(q => tokenIds.Contains(q.Name)).Select(q => q.Id).ToList();
            var userGroupTokenIds = userGroupTokenBiz.FetchMulti(q => q.UserGroup_Id == userGroupId).Select(q => q.Token_Id).ToList();

            var IdsToDelete = userGroupTokenIds.Except(selectedIds);
            foreach (var tokenId in IdsToDelete)
            {
                userGroupTokenBiz.Delete(q => q.Token_Id == tokenId && q.UserGroup_Id == userGroupId);
                var token = tokenBiz.FirstOrDefault(x => x.Id == tokenId);
                RemoveClaim(UserId, "UserRight", token.Name);
            }

            var IdsToAdd = selectedIds.Except(userGroupTokenIds);
            foreach (var tokenId in IdsToAdd)
            {
                userGroupTokenBiz.Create(new Common.Models.UserGroupToken()
                {
                    Token_Id = tokenId,
                    UserGroup_Id = userGroupId,
                });
                var token = tokenBiz.FirstOrDefault(x => x.Id == tokenId);

            }
            return null;
        }

        public ActionResult GetUserGroupTokens(int userGroupId)
        {
            var tokens = userGroupTokenBiz.FetchMulti(q => q.UserGroup_Id == userGroupId).Select(q => new { q.Token.Name }).ToList();
            return Json(tokens, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadUserGroupForDropDown()
        {
            var userGroups = userGroupBiz.FetchMulti().Select(q => new { q.Id, q.Name }).ToList();
            return Json(userGroups, JsonRequestBehavior.AllowGet);
        }

        #region [private method]
        private List<TokenSelectViewModel> getAuthorizedActions()
        {
            return doReflectionStuff();
        }
        private List<TokenSelectViewModel> doReflectionStuff()
        {
            Assembly asm = Assembly.GetAssembly(typeof(MvcApplication));

            var controllerLists = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .Where(m => m.GetCustomAttributes(typeof(TokenCategoryAttribute), true).Any())
                    .ToList();

            var tokensdict = new Dictionary<string, TokenSelectViewModel>();
            var tokenCategoriesdict = new Dictionary<string, string>();
            foreach (var controller in controllerLists)
            {
                StringBuilder actionFullName = new StringBuilder();
                if (controller.Namespace != null)
                    actionFullName.Append(controller.Namespace);
                actionFullName.Append(controller.Name);

                var areaName = string.Empty;
                if (!string.IsNullOrEmpty(controller.Namespace) && (controller.Namespace.Count(q => q == '.') > 2))
                    areaName = controller.Namespace.Split('.')[3] + "_";

                var methods = controller.GetMethods().Where(m => m.GetCustomAttributes(typeof(ClaimBasedAuthorzationAttribute), true).Any()).ToList();

                var tokenCategoryAttr = (TokenCategoryAttribute)controller.GetCustomAttribute(typeof(TokenCategoryAttribute), false);
                var categoryName = tokenCategoryAttr.CategoryName;
                var categoryTitle = tokenCategoryAttr.CategoryTitle;

                var i = 0;
                foreach (var method in methods)
                {
                    actionFullName.Append(method.Name);

                    var attr = (ClaimBasedAuthorzationAttribute)method.GetCustomAttribute(typeof(ClaimBasedAuthorzationAttribute), false);

                    //var tokenName = areaName  + categoryName + "_" + controller.Name + "_" + method.Name;
                    var tokenName = areaName + categoryName + "_" + method.Name;

                    var model = new TokenSelectViewModel()
                    {
                        Id = tokenName,
                        Name = attr.TokenName,
                        AreaName = areaName,
                        CategoryName = categoryName,
                        CategoryTitle = categoryTitle,
                        Checked = false
                    };
                    list.Add(model);
                    i++;



                    if (string.IsNullOrWhiteSpace(tokenName))
                        throw new Exception("tokenName is required");

                    if (string.IsNullOrWhiteSpace(categoryTitle))
                        throw new Exception("tokenName is categoryTitle");

                    //tokensdict.Add(tokenName, categoryTitle);
                    tokensdict.Add(tokenName, model);
                }

                if (string.IsNullOrWhiteSpace(categoryName))
                    throw new Exception("tokenName is categoryName");

                if (string.IsNullOrWhiteSpace(categoryTitle))
                    throw new Exception("tokenName is categoryTitle");

                tokenCategoriesdict.Add(categoryName, categoryTitle);
            }

            syncTokenCategories(tokenCategoriesdict);
            syncTokens(tokensdict);

            var sortedList = list.OrderBy(q => q.AreaName).ToList();
            return sortedList;
        }
        private void syncTokenCategories(Dictionary<string, string> tokenCategoriesdict)
        {
            #region [Sync categories]
            var tokenCategories = tokenCategoryBiz.FetchMulti().ToList();
            var tokenCategoryToDelete = tokenCategories.Where(q => !tokenCategoriesdict.ContainsKey(q.Name)).ToList();
            foreach (var category in tokenCategoryToDelete)
            {
                tokenCategoryBiz.Delete(q => q.Name == category.Name);
            }
            var categoriesName = tokenCategories.Select(q => q.Name);
            var tokenCategoryToAdd = tokenCategoriesdict.Where(q => !categoriesName.Contains(q.Key)).ToList();
            foreach (var category in tokenCategoryToAdd)
            {
                tokenCategoryBiz.Create(new Common.Models.TokenCategory()
                {
                    Name = category.Key,
                    PersianName = category.Value
                });
            }
            #endregion
        }
        private void syncTokens(Dictionary<string, TokenSelectViewModel> tokensdict)
        {
            #region [Sync Tokens]
            var tokenCategories = tokenCategoryBiz.FetchMulti().ToList();
            var tokens = tokenBiz.FetchMulti().ToList();
            var tokensToDelete = tokens.Where(q => !tokensdict.ContainsKey(q.Name)).ToList();
            foreach (var token in tokensToDelete)
            {
                tokenBiz.Delete(q => q.Name == token.Name);
            }
            var tokensName = tokens.Select(q => q.Name);
            var tokensToAdd = tokensdict.Where(q => !tokensName.Contains(q.Key)).ToList();
            foreach (var token in tokensToAdd)
            {
                var categoryName = string.Empty;
                var key = token.Key.ToLower();

                tokenBiz.Create(new Common.Models.Token()
                {
                    Name = token.Key,
                    AreaName = token.Value.AreaName,
                    PersianName = token.Value.Name,
                    TokenCategory_Id = tokenCategories.First(q => q.Name.ToLower() == token.Value.CategoryName.ToLower()).Id
                });
            }
            #endregion
        }
        #endregion

    }
}