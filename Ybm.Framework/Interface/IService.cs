using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Interface
{
    public interface IService<T> : ISaveService<T>, IDeleteService<T>, IFetchService<T> where T : class
    {
        int RunQuery(string query, params object[] parameters);
        void SaveChanges();
    }
}
