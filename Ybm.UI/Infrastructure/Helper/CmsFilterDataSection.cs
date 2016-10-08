using Ybm.Business;
using Ybm.Framework;
using Ybm.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ybm.Framework;

namespace Ybm.UI
{
    public static class YbmFilterData
    {
        public static MvcHtmlString CmsFilerDataSection(this HtmlHelper helper, List<CustomFilterDescriptor> filterControlsModel, int? User_Id)
        {
            TagBuilder tag = new TagBuilder("FilterData");

            StringBuilder html = new StringBuilder();

            html.AppendLine($@"<div class=""form-group row"">");

            foreach (var item in filterControlsModel)
            {
                html.AppendLine($@"<div class=""col-lg-2"">");
                html.AppendLine($@"<label class=""form-control-label control-label"" for='{ item.Member }'>{ item.Name}</label>");
                html.AppendLine($@"</div>");

                if (item.Member.Replace("FilterField_", "").Contains("_"))
                {
                    var member = item.Member.Replace("FilterField_", "");
                    var tableName = member.Split('_')[0];
                    var propertyName = member.Split('_')[1];
                    GetTableItemsAsList(User_Id, html, item, tableName);
                }
                else
                if ((item.MemberType == typeof(bool)) || (item.MemberType == typeof(bool?)))
                {
                    html.AppendLine($@"<div class=""col-lg-4"">");
                    html.AppendLine($@"<select id =""{ item.Member }"" class=""input-large form-control FilterDescriptor"">");
                    html.AppendLine($@"<option value="""" selected >❓</option>");
                    html.AppendLine($@"<option value =""true"" >✅</option>");
                    html.AppendLine($@"<option value =""false"" >❌</option>");
                    html.AppendLine($@"</select>");
                    //html.AppendLine($@"<input class=""form-control FilterDescriptor"" id='{ item.Member }' name='{ item.Member }' type=""checkbox"" value="""">");
                    html.AppendLine($@"</div>");
                }
                else
                if (item.MemberType == typeof(string))
                {
                    html.AppendLine($@"<div class=""col-lg-4"">");
                    html.AppendLine($@"<input class=""form-control FilterDescriptor"" id='{ item.Member }' name='{ item.Member }' type=""text"" value="""">");
                    html.AppendLine($@"</div>");
                }
                else
                if ((item.MemberType == typeof(int)) || (item.MemberType == typeof(int?))
                   || (item.MemberType == typeof(decimal)) || (item.MemberType == typeof(decimal?))
                   )
                {
                    html.AppendLine($@"<div class=""col-lg-4"">");
                    html.AppendLine($@"<input type=""number"" class=""form-control FilterDescriptor"" id='{ item.Member }' name='{ item.Member }'  value="""">");
                    html.AppendLine($@"</div>");
                }
                else
                if ((item.MemberType == typeof(DateTime)) || (item.MemberType == typeof(DateTime?)))
                {
                    html.AppendLine($@"<div class=""col-lg-4"">");
                    html.AppendLine($@"<input class=""form-control FilterDescriptor"" id='{ item.Member }' name='{ item.Member }' type=""text"" value="""">");
                    html.AppendLine($@"</div>");

                    html.AppendLine($@"<script>");
                    html.AppendLine($@"$(document).ready(function() {{");
                    html.AppendLine($@"$(""#{ item.Member }"").kendoDatePicker({{");
                    html.AppendLine($@"culture: ""fa-IR"",");
                    html.AppendLine($@"format: ""yyyy/MM/dd"",");
                    html.AppendLine($@"close: function() {{");
                    html.AppendLine($@"}}");
                    html.AppendLine($@"}});");
                    html.AppendLine($@"}});");
                    html.AppendLine($@"</script>");
                }




            }
            html.AppendLine($@"</div>");
            html.AppendLine($@"<button type = ""button"" title=""Test"" ng-click=""FilterDescriptorClick()"" > جستجو</button>");


            return MvcHtmlString.Create(html.ToString());

        }

        private static void GetTableItemsAsList(int? User_Id, StringBuilder html, CustomFilterDescriptor item, string tableName)
        {
            dynamic list;
            //if (tableName == "Project")
            //{
            //    IProjectBusiness instance = ServiceFactory.CreateInstance<IProjectBusiness>();
            //    list = instance.FetchMulti().Where(q => q.Employer_Id == User_Id).Select(q => new { q.Id, q.Name }).ToList();
            //}
            //else
            //if (tableName == "Grade")
            //{
            //    IGradeBusiness instance = ServiceFactory.CreateInstance<IGradeBusiness>();
            //    list = instance.FetchMulti().Select(q => new { q.Id, q.Name }).ToList();
            //}
            //else
            //if (tableName == "User")
            //{
            //    IUserBusiness instance = ServiceFactory.CreateInstance<IUserBusiness>();
            //    list = instance.FetchMulti().Select(q => new { q.Id, Name = q.FirstName + " " + q.LastName }).ToList();
            //}
            //else
            //if (tableName == "Complaint")
            //{
            //    IComplaintBusiness instance = ServiceFactory.CreateInstance<IComplaintBusiness>();
            //    list = instance.FetchMulti(q => q.Complaintor_Id == User_Id).Select(q => new { q.Id, Name = q.Description }).ToList();
            //}
            //else
            //if (tableName == "Skill")
            //{
            //    ISkillBusiness instance = ServiceFactory.CreateInstance<ISkillBusiness>();
            //    list = instance.FetchMulti().Select(q => new { q.Id, Name = q.Name }).ToList();
            //}
            //else
            //if (tableName == "UserGroup")
            //{
            //    IUserGroupBusiness instance = ServiceFactory.CreateInstance<IUserGroupBusiness>();
            //    list = instance.FetchMulti().Select(q => new { q.Id, q.Name }).ToList();
            //}
            //else
            //    list = new List<Common.Models.Project>();







            html.AppendLine($@"<div class=""col-lg-4"">");
            html.AppendLine($@"<select id =""{ item.Member }"" class=""input-large form-control FilterDescriptor"">");
            html.AppendLine($@"<option value = """" > یک گزینه را انتخاب کنید </option>");
            //foreach (var itm in list)
            //{
            //    html.AppendLine($@"<option value= ""{itm.Id}"" > {itm.Name}</option>");
            //}
            html.AppendLine($@"</select>");
            html.AppendLine($@"</div>");
        }
    }
}