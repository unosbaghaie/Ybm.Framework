using Ybm.Framework.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{

    public class ViewModel
    {
        public ViewModel(string sourcePath, string destinationPath)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;

        }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public void Run()
        {
            StringBuilder str = new StringBuilder();


            var assembly = System.Reflection.Assembly.LoadFile(SourcePath);
            var types = new HashSet<Type>(assembly.GetTypes().ToList());


            foreach (Type type in types)
            {
                if (!Form1.FileNames.Contains(type.Name))
                    continue;


                var nameSpace = "Cms.UI.Infrastructure.ViewModels";


                str.Append($@"
using System;
using System.ComponentModel.DataAnnotations;

namespace {nameSpace}
{{
    public partial class  {type.Name}ViewModel 
    {{
");

                foreach (var property in type.GetProperties())
                {
                    if (property.PropertyType == typeof(DateTime?))
                        str.AppendLine($@"        public DateTime?  {property.Name} {{ set; get;}}");
                    else if (property.PropertyType == typeof(int?))
                        str.AppendLine($@"        public int?  {property.Name} {{ set; get;}}");
                    else if (property.PropertyType == typeof(long?))
                        str.AppendLine($@"        public long?  {property.Name} {{ set; get;}}");
                    else if (property.PropertyType == typeof(bool?))
                        str.AppendLine($@"        public bool?  {property.Name} {{ set; get;}}");
                    else if (property.PropertyType == typeof(byte?))
                        str.AppendLine($@"        public byte?  {property.Name} {{ set; get;}}");
                    else if (property.PropertyType == typeof(double?))
                        str.AppendLine($@"        public double?  {property.Name} {{ set; get;}}");
                    else if (property.PropertyType == typeof(decimal?))
                        str.AppendLine($@"        public decimal?  {property.Name} {{ set; get;}}");
                    else
                        if (!property.PropertyType.Name.ToString().Contains("ICollection") &&
                        (!property.PropertyType.Name.ToString().Contains("Nullable")))
                    {
                        str.AppendLine(@"[Required(ErrorMessage = ""مقدار این فیلد نمی تواند خالی باشد"")]");
                        str.AppendLine($@"        public {property.PropertyType.Name} {property.Name} {{ set; get;}}");
                    }
                }


                str.Append($@"
    }}
}}
");


                var fileName = DestinationPath + type.Name + "ViewModel.cs";
                File.WriteAllText(fileName, str.ToString());
                str.Clear();
            }
        }
    }
}