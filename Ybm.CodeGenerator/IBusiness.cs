using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{
    public class IBusiness
    {
        public IBusiness(string sourcePath,string destinationPath)
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

                if (fileNameWithoutExtension.StartsWith("CmsContext"))
                    continue;


                var className = fileNameWithoutExtension;
                var nameSpace = "Cms.Business";


            str.Append($@"
using Ybm.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {nameSpace}
{{
    public interface I{className}Business : Framework.Interface.IService<{className}>
    {{
    }}
}}
");


                var fileName = DestinationPath +"I"+ fileNameWithoutExtension + "Business.cs";
                File.WriteAllText(fileName, str.ToString());
                str.Clear();




        }

        }
    }
}
