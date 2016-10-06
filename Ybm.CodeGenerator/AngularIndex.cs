using Ybm.Framework.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.CodeGenerator
{
    public class AngularIndex
    {
        public AngularIndex(string sourcePath, string destinationPath)
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
            var types = new HashSet<Type>(assembly.GetTypes().Where(q => q.IsSubclassOf(typeof(Entity))).ToList());


            foreach (Type type in types)
            {


                str.Append($@"app.controller('{type.Name}IndexController', [""$scope"", ""serviceBaseAngular"", ""$compile"", function ($scope, serviceBaseAngular, $compile) {{



}}]); ");

                var destPath = DestinationPath + "\\" + type.Name;
                if (!Directory.Exists(destPath))
                    Directory.CreateDirectory(destPath);

                var fileName = destPath + "\\" + type.Name + "IndexController.js";
                File.WriteAllText(fileName, str.ToString());
                str.Clear();
            }
        }
    }
}
