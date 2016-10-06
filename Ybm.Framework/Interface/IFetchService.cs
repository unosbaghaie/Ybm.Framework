using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Interface
{
    public interface IFetchService<T> where T : class
    {
        IQueryable<T> FetchAll();

        IQueryable<T> FetchAll<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate,
            Expression<Func<T, S>> orderByExpression, bool ascending = true);

        IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null);
        IPagedList<T> FetchMulti(int pageIndex, int pageSize, Expression<Func<T, int>> orderbyExpression, Expression<Func<T, bool>> predicate = null,bool ascending = true);

        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);

        T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        int Count(Expression<Func<T, bool>> predicate = null);

        T FetchFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate);
    }
}
