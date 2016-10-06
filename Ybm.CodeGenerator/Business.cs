using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{
    public class Business
    {
        public Business(string sourcePath,string destinationPath)
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

                if (fileNameWithoutExtension.StartsWith("CMSContext"))
                    continue;

                var className = fileNameWithoutExtension;
                var nameSpace = "Cms.Business";


            str.Append($@"
using Ybm.Common.Models;
using Ybm.Framework;
using Ybm.Framework.Attribute;
using Ybm.Framework.Service;

namespace {nameSpace}
{{
    public partial class {className}Business : Service<{className}>, I{className}Business
    {{
        public {className}Business()
            :base(ContainerManager.Container.Resolve<CMSContext>())
        {{
                base.PopulateEvents(this);
        }}
    }}
}}");


                var fileName = DestinationPath + fileNameWithoutExtension + "Business.cs";
                File.WriteAllText(fileName, str.ToString());
                str.Clear();




        }

        }
    }
}
