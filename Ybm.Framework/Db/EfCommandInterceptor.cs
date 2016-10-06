using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework
{
    public class EfCommandInterceptor : IDbCommandInterceptor
    {
        public static void Log(string comm, string message)
        {
            Console.WriteLine("Intercepted: {0}, Command Text: {1} ", comm, message);
        }

        public void NonQueryExecuted(DbCommand command,
           DbCommandInterceptionContext<int> interceptionContext)
        {
            Log("NonQueryExecuted: ", command.CommandText);
        }

        public void NonQueryExecuting(DbCommand command,
           DbCommandInterceptionContext<int> interceptionContext)
        {
            Log("NonQueryExecuting: ", command.CommandText);
        }

        public void ReaderExecuted(DbCommand command,
           DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log("ReaderExecuted: ", command.CommandText);
        }

        public void ReaderExecuting(DbCommand command,
           DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log("ReaderExecuting: ", command.CommandText);
        }

        public void ScalarExecuted(DbCommand command,
           DbCommandInterceptionContext<object> interceptionContext)
        {
            Log("ScalarExecuted: ", command.CommandText);
        }

        public void ScalarExecuting(DbCommand command,
           DbCommandInterceptionContext<object> interceptionContext)
        {
            Log("ScalarExecuting: ", command.CommandText);
        }
    }
}
