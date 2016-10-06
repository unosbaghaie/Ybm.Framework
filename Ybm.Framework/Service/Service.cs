using EntityFramework.Extensions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ybm.Framework.Service
{
    public class Service<T> : Interface.IService<T> where T : class
    {

        #region [Eventing Section]
        Interface.IService<T> implementation;
        #region [Events]
        public virtual event System.EventHandler<EntitySavingEventArgs<T>> BeforeSavingRecord;

        public virtual event System.EventHandler<EntitySavingEventArgs<T>> SavingRecord;

        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordSaved;



        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> BeforeDeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> DeletingRecord;
        public virtual event System.EventHandler<EntityDeletingEventArgs<T>> RecordDeleted;


        public virtual event System.EventHandler<EntitySavingEventArgs<T>> UpdatingRecord;

        public virtual event System.EventHandler<EntitySavingEventArgs<T>> RecordUpdated;
        #endregion
        public void PopulateEvents(Interface.IService<T> _implementation)
        {
            implementation = _implementation;

            implementation.BeforeSavingRecord += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnBeforeSavingRecord);
            implementation.SavingRecord += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnSavingRecord);
            implementation.RecordSaved += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordSaved);


            implementation.BeforeDeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnBeforeDeletingRecord);
            implementation.DeletingRecord += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnDeletingRecord);
            implementation.RecordDeleted += new System.EventHandler<EntityDeletingEventArgs<T>>(this.OnRecordDeleted);


            implementation.UpdatingRecord += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnUpdatingRecord);
            implementation.RecordUpdated += new System.EventHandler<EntitySavingEventArgs<T>>(this.OnRecordUpdated);


        }
        #region [Virtual Mothods]


        public virtual void OnBeforeSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnSavingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordSaved(object sender, EntitySavingEventArgs<T> e)
        {
        }


        public virtual void OnBeforeDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnDeletingRecord(object sender, EntityDeletingEventArgs<T> e)
        {
        }
        public virtual void OnRecordDeleted(object sender, EntityDeletingEventArgs<T> e)
        {
        }

        public virtual void OnUpdatingRecord(object sender, EntitySavingEventArgs<T> e)
        {
        }
        public virtual void OnRecordUpdated(object sender, EntitySavingEventArgs<T> e)
        {
        }
        #endregion 
        #endregion

        private readonly DbContext _dbContext;
        public Service(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Create(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                BeforeSavingRecord?.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
                _dbContext.Set(typeof(T)).Add(item);
                SavingRecord?.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
                _dbContext.SaveChanges();
                RecordSaved?.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }

        }

        /// <summary>
        /// It doesn't support Creating Events. Instead use Create(T model)
        /// </summary>
        /// <param name="items"></param>
        public virtual void CreateMulti(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("item");

            _dbContext.Set(typeof(T)).AddRange(items);
            _dbContext.SaveChanges();
        }

        public virtual void Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            //item.Datestamp = DateTime.Now;
            //_dbContext.Set(item.GetType()).Attach(item);

            _dbContext.Entry(item).State = EntityState.Modified;
            UpdatingRecord?.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

            _dbContext.SaveChanges();
            RecordUpdated?.Invoke(this, new EntitySavingEventArgs<T>() { SavedEntity = item });

        }
        public virtual void SaveChanges()
        {
            //Console.WriteLine("_dbContext : " + _dbContext.GetHashCode() + "  sender : " + sender);
            try
            {
                _dbContext.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }

        }

        /// <summary>
        /// It doesn't support deleting events. Instead Use Delete(T item)
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            _dbContext.Set<T>().Where(predicate).Delete();
        }

        public virtual void Delete(T item)
        {
            BeforeDeletingRecord?.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });

            _dbContext.Set<T>().Attach(item);
            _dbContext.Set<T>().Remove(item);
            DeletingRecord?.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
            _dbContext.SaveChanges();
            RecordDeleted?.Invoke(this, new EntityDeletingEventArgs<T>() { SavedEntity = item });
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }
            _dbContext.SaveChanges();

        }

        public virtual IQueryable<T> FetchAll()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<T> FetchMulti(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().AsNoTracking() :
                 _dbContext.Set<T>().AsNoTracking().Where(predicate);
        }

        public virtual IPagedList<T> FetchMulti(int pageIndex, int pageSize, Expression<Func<T, int>> orderbyExpression, Expression<Func<T, bool>> predicate = null, bool ascending = true)
        {
            pageIndex--;

            if (pageIndex < 0)
                throw new ArgumentException("Page index must be greater or equal to 0", "pageIndex");
            if (pageSize <= 0)
                throw new ArgumentException("Page size must be greater then 0", "pageSize");


            var objectSet = _dbContext.Set<T>();

            int totalItemCount = Count(predicate);

            IQueryable<T> query;
            if (predicate != null)
                query = objectSet.Where(predicate);
            else
                query = objectSet;

            if (orderbyExpression != null && ascending == false)
                query = query.OrderByDescending(orderbyExpression);
            else
                query = query.OrderBy(orderbyExpression);

            var items = query.Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            pageIndex++;
            return new StaticPagedList<T>(items, pageIndex, pageSize, totalItemCount);
        }

        public virtual IQueryable<T> FetchAll<S>(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate, Expression<Func<T, S>> orderByExpression, bool ascending = true)
        {
            pageIndex--;
            if (pageIndex < 0)
                throw new ArgumentException("Page index must be greater or equal to 0", "pageIndex");
            if (pageSize <= 0)
                throw new ArgumentException("Page size must be greater then 0", "pageSize");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            if (orderByExpression == null)
                throw new ArgumentNullException("orderByExpression");

            var objectSet = _dbContext.Set<T>();

            var items = (ascending)
                ? objectSet
                    .OrderBy(orderByExpression)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                : objectSet
                    .OrderByDescending(orderByExpression)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize);

            pageIndex++;
            return items;
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbContext.Set<T>().FirstOrDefault() : _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public virtual T FirstOrDefaultWithReload(Expression<Func<T, bool>> predicate)
        {
            var entity = _dbContext.Set<T>().FirstOrDefault(predicate);
            if (entity == null)
                return default(T);
            _dbContext.Entry(entity).Reload();
            return entity;
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().SingleOrDefault(predicate);
        }


        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ?
            _dbContext.Set<T>().Count() :
            _dbContext.Set<T>().Count(predicate);

        }


        public virtual int BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updatePredicate)
        {
            return _dbContext.Set<T>().Where(predicate).Update(updatePredicate);
        }

        public virtual int RunQuery(string query, params object[] parameters)
        {
            var result = _dbContext.Database.SqlQuery<int>(query, parameters).ToList();
            return result[0];

        }

        public T FetchFirstOrDefaultAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        }

        public void UpdateWithAttach(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _dbContext.Set(item.GetType()).Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        //public List<Tuple<Entity, EntityActionType>> Save(ref T record, Action<Entity, List<Tuple<Entity, EntityActionType>>> onSavingRecord, Action<Entity, List<Tuple<Entity, EntityActionType>>> onRecordSaved)
        //{
        //    return crea(ref record, true, onSavingRecord, onRecordSaved);
        //}
    }
}
