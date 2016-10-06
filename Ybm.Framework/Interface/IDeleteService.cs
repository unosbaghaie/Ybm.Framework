using Ybm.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Interface
{
    public interface IDeleteService<T> where T : class
    {
        event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;

        event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;

        void Delete(T item);
        void Delete(IEnumerable<T> entities);

        void Delete(Expression<Func<T, bool>> predicate);


    }

}
