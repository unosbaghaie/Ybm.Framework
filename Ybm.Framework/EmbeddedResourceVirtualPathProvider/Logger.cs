using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ybm.Framework.EmbeddedResourceVirtualPathProvider
{
    public static class Logger
    {
        static Logger()
        {
            LogWarning = (message, ex) => { };
        }

        public static Action<string, Exception> LogWarning { get; set; }
    }
}
