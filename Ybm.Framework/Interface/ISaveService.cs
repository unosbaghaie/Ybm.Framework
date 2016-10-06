using Ybm.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Interface
{
    public interface ISaveService<T> where T : class
    {
        event System.EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> SavingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> RecordSaved;

        event System.EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;
        event System.EventHandler<EntitySavingEventArgs<T>> RecordUpdated;

        void Create(T item);
        void Update(T item);

        void CreateMulti(IEnumerable<T> items) ;
        int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate);
        void UpdateWithAttach(T item);
    }
}
