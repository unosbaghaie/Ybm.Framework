using Ybm.Framework.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{
    public class IndexView
    {
        public IndexView(string sourcePath, string destinationPath)
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

                str.Append($@"
@{{
    ViewBag.Title = ""Index"";
    Layout = ""~/Views/Shared/_Layout.cshtml"";
            }}

<h2>Index</h2>

@section Scripts
{{
    < script src=""~/app/areas/User/{type.Name}/{type.Name}Index.js""></script>

}}
@using Kendo.Mvc.UI;
<div dir=""rtl"" ng-controller=""{type.Name}IndexController"">

    @Html.ActionLink(""جدید"", ""{type.Name}Create"", ""{type.Name}"", new {{ @class = ""btn btn-default"" }})

    @(Html.Kendo().Grid<Ybm.UI.Infrastructure.ViewModels.{type.Name}ViewModel>()
                      .Name(""{type.Name}Grid"")
                      .Columns(columns =>
                      {{
");






                foreach (var property in type.GetProperties())
                {
                    str.AppendLine($@"        columns.Bound(p => p.{property.Name} );");
                    
                }

                          
    str.Append($@"               columns.Command(command =>
                          {{
                              command.Custom(""حذف"").Click(""Delete"");
                          }});

                      }})

                      .Pageable()
        .Scrollable(q => q.Enabled(false))
    .DataSource(dataSource => dataSource
        .Ajax()
         .Model(model =>
         {{
             model.Id(p => p.Id);

         }})
        .Read(read => read.Action(""Read"", ""{type.Name}"", ""User""))
    )
    )

    <button onclick=""CreateNew()""> جدید </button>
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
                        str.AppendLine($@"        public {property.PropertyType.Name} {property.Name} {{ set; get;}}");
                }


                str.Append($@"
    }}
}}
");
                var destPath = DestinationPath + "\\" + type.Name;
                if (!Directory.Exists(destPath))
                    Directory.CreateDirectory(destPath);

                var fileName = destPath +"\\"+ type.Name + "Index.cshtml";
                File.WriteAllText(fileName, str.ToString());
                str.Clear();
            }
        }

    }
}
