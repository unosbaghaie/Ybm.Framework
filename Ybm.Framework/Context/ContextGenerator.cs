using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Context
{
    public class ContextGenerator
    {
        public void Generate(List<string> entities, params Type[] types)
        {
            StringBuilder code = new StringBuilder();

            code.AppendLine(@"
           using System.Data.Entity;
           using System.Data.Entity.Core.EntityClient;
           using Ybm.Common.Models;
           using Ybm.Common.Models.Mapping;
           using AT.Common.Models;
           using AT.Common.Models.Mapping;

           namespace Models
           {
                public partial class OnlineShopContext : DbContext
                {
                    static OnlineShopContext()
                    {
                        Database.SetInitializer<OnlineShopContext>(null);
                    }

                    public OnlineShopContext()
                        : base(""Name = OnlineShopContext"")
                    {
                        }
                ");


            foreach (var entity in entities)
            {
                code.AppendLine($@"public DbSet<{entity}> {entity}s {{ get; set; }}");
            }

            code.AppendLine(@"protected override void OnModelCreating(DbModelBuilder modelBuilder)");
            code.AppendLine(@"{");

            foreach (var entity in entities)
            {
                code.AppendLine($@"modelBuilder.Configurations.Add(new {entity}Map());");
            }
            code.AppendLine(@"}");

            code.AppendLine(@"}");

            code.AppendLine(@"}");





            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            // Reference to System.Drawing library
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Data.Entity.dll");
            parameters.ReferencedAssemblies.Add("System.ComponentModel.dll");


            foreach (var type  in types)
            {
                parameters.ReferencedAssemblies.Add(type.Assembly.Location);
            }

            parameters.ReferencedAssemblies.Add(typeof(DbSet).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(DbContext).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(IQueryable).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(IQueryable<>).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(System.ComponentModel.IListSource).Assembly.Location);

            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = false;
            parameters.OutputAssembly = "OutputAssembly.dll";

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code.ToString());

            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }
        }

    }
}
