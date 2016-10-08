using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{
    public class MvcController
    {
        public MvcController(string sourcePath, string destinationPath)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;

        }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public void Run()
        {
            StringBuilder str = new StringBuilder();



            var files = Directory.GetFiles(SourcePath, "*.cs", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {



                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);

                if (fileNameWithoutExtension.StartsWith("AspNet"))
                    continue;

                if (fileNameWithoutExtension.StartsWith("YbmContext"))
                    continue;

                var className = fileNameWithoutExtension;
                var nameSpace = "Ybm.Business";

                

                str.Append($@"public class {className}Controller : MvcController
    {{
        I{className}Business {className.ToLower()}Biz = ServiceFactory.CreateInstance<I{className}Business>();

        #region Load views
        public ActionResult Index()
        {{
            return View();
        }}

        public ActionResult {className}Update(int id)
        {{
            var model = {className.ToLower()}Biz.FirstOrDefault(q => q.Id == id);
            return View(model.GetViewModel());
        }}

        public ActionResult {className}Create()
        {{
            return View();
        }}

        #endregion

        #region operations
     
        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request, byte status = 1)
        {{
            IPagedList<{className}> list;
            list = {className.ToLower()}Biz.FetchMulti(request.Page, request.PageSize, q => q.Id, null);
            var count = {className.ToLower()}Biz.Count();
            return Json(list.ToList().Select(q => q.GetViewModel()).ToDataSourceResult(request, count), JsonRequestBehavior.AllowGet);
        }}

        [HttpPost]
        public void Update({className}ViewModel model)
        {{
            var domain = {className.ToLower()}Biz.FirstOrDefault(q => q.Id == model.Id);
            domain = model.GetDomainModel();
            {className.ToLower()}Biz.Update(domain);
        }}
        public virtual ActionResult Delete([DataSourceRequest] DataSourceRequest request, int Id)
        {{
            {className} s = new {className}() {{ Id = Id }};
            {className.ToLower()}Biz.Delete(s);
            return Read(request, 0);
        }}

        #endregion
    }}");

                var destPath = DestinationPath + "\\" + className;
                if (!Directory.Exists(destPath))
                    Directory.CreateDirectory(destPath);

                var fileName = destPath + "\\" + className + "Controller.cs";
                File.WriteAllText(fileName, str.ToString());
                str.Clear();




            }

        }
    }
}
