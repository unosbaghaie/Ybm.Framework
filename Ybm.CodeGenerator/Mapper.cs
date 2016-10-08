using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{
    public class Mapper
    {
        public Mapper(string sourcePath,string destinationPath)
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
                var nameSpace = "Ybm.UI.Infrastructure.Mapper";


                str.Append($@"using AutoMapper;
using Ybm.Common.Models;
using Ybm.Framework;
using Ybm.UI.Infrastructure.ViewModels;
using System;

namespace Ybm.UI.Infrastructure.Mapper
{{
    public  static class {className}Mapper
    {{
        private static IMapper GetViewModelMapper {{ get; set; }}
        private static IMapper GetDomainModelMapper {{ get; set; }}
        public static {className}ViewModel GetViewModel(this {className} domain)
        {{
            if (GetViewModelMapper == null)
            {{
                var config = new MapperConfiguration(cfg =>
                {{
                    //cfg.CreateMap<string, int>().ConvertUsing<TypeTypeConverter>();
                    cfg.CreateMap<{className}, {className}ViewModel>()
                    ;
                    //.ForMember(q => q.Description, o => o.Ignore())
                }}); 
                 GetViewModelMapper = config.CreateMapper();
            }}
            var dest = GetViewModelMapper.Map<{className}, {className}ViewModel>(domain);
            return dest; 
        }}
        public static {className} GetDomainModel(this {className}ViewModel viewModel)
        {{
            if (GetDomainModelMapper == null)
            {{
                var config = new MapperConfiguration(cfg =>
                {{
                    cfg.CreateMap<{className}ViewModel, {className}>();
                }});

                GetDomainModelMapper = config.CreateMapper();
            }}
            var dest = GetDomainModelMapper.Map<{className}ViewModel,{className}>(viewModel);
            return dest;
        }}
    }}
    //public class TypeTypeConverter : ITypeConverter<string, int>
    //{{
    //    public int Convert(string source, int destination, ResolutionContext context)
    //    {{
    //        return System.Convert.ToInt32(source);
    //    }}
    //}}
}}");


                var fileName = DestinationPath + fileNameWithoutExtension + "Mapper.cs";
                File.WriteAllText(fileName, str.ToString());
                str.Clear();




        }

        }
    }
}
