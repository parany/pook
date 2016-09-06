using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Pook.Data.Entities;
using Pook.Data.Exceptions;
using Pook.Data.Repositories.Interface;

namespace Pook.Data.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Content
    {
        public List<Expression<Func<T, object>>> NavigationProperties;
        protected List<Expression<Func<T, object>>> IgnoreProperties;
        public PagingSettings PagingSettings;
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy;

        public GenericRepository()
        {
            SetSortExpression(list => list.OrderByDescending(p => p.CreatedOn));
        }

        public virtual void SetPagingSettings(PagingSettings pagingSettings)
        {
            PagingSettings = pagingSettings;
        }

        public void SetSortExpression(Func<IQueryable<T>, IOrderedQueryable<T>> sortExpression)
        {
            OrderBy = sortExpression;
        }

        public virtual void SetNavigationProperties(params Expression<Func<T, object>>[] navigationProperties)
        {
            NavigationProperties?.Clear();
            AddNavigationProperties(navigationProperties);
        }

        public virtual void AddNavigationProperties(params Expression<Func<T, object>>[] navigationProperties)
        {
            foreach (var prop in navigationProperties)
            {
                AddNavigationProperty(prop);
            }
        }

        public virtual void AddIgnoreProperties(params Expression<Func<T, object>>[] ignoreProperties)
        {
            if (ignoreProperties != null)
            {
                foreach (var prop in ignoreProperties)
                {
                    AddIgnoreProperty(prop);
                }
            }
        }

        public virtual void AddIgnoreProperty(Expression<Func<T, object>> ignoreProperty)
        {
            if (IgnoreProperties == null) { IgnoreProperties = new List<Expression<Func<T, object>>>(); }
            IgnoreProperties.Add(ignoreProperty);
        }

        public virtual void AddNavigationProperty(Expression<Func<T, object>> navigationProperty)
        {
            if (NavigationProperties == null) { NavigationProperties = new List<Expression<Func<T, object>>>(); }
            NavigationProperties.Add(navigationProperty);
        }

        public virtual IList<T> GetAll()
        {
            List<T> list;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                // Apply eager loading
                if (NavigationProperties != null && NavigationProperties.Count > 0)
                {
                    dbQuery = NavigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                }

                // Apply sorting
                dbQuery = OrderBy(dbQuery);

                list = dbQuery
                  .AsNoTracking()
                  .ToList();
            }


            return list;
        }

        public virtual IList<T> GetList(Expression<Func<T, bool>> where)
        {
            List<T> list;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                context.Configuration.ProxyCreationEnabled = false;

                // Apply eager loading
                if (NavigationProperties != null && NavigationProperties.Count > 0)
                {
                    dbQuery = NavigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                }

                dbQuery = dbQuery
                        .AsNoTracking()
                        .Where(where);

                // Set total number of records
                if (PagingSettings != null) { PagingSettings.TotalNumberOfRecords = dbQuery.Count(); }

                // Apply sorting
                dbQuery = OrderBy(dbQuery);

                // Retrieve data
                if (PagingSettings != null)
                {
                    dbQuery = dbQuery
                        .Skip((PagingSettings.Page - 1) * PagingSettings.PageSize)
                        .Take(PagingSettings.PageSize);
                }

                list = dbQuery.ToList();

                // Set number of records
                if (PagingSettings != null) { PagingSettings.NumberOfRecords = list.Count; }
            }

            return list;
        }

        public virtual int GetCount(Expression<Func<T, bool>> where)
        {
            int count;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                dbQuery = dbQuery.AsNoTracking();

                if (where != null)
                    dbQuery = dbQuery.Where(where);

                // Set total number of records
                count = dbQuery.Count();
            }
            return count;
        }

        public virtual bool Exists(Guid id)
        {
            bool exists;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                exists = dbQuery
                    .AsNoTracking()
                    .Any(obj => obj.Id == id);
            }

            return exists;
        }

        public virtual T GetFirst()
        {
            T item;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault();
            }

            return item;
        }

        public virtual T GetLastCreated()
        {
            T item;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                item = dbQuery
                    .AsNoTracking()
                    .OrderByDescending(obj => obj.CreatedOn)
                    .FirstOrDefault();
            }

            return item;
        }

        public virtual T GetSingle(Expression<Func<T, bool>> where)
        {
            T item;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                // Apply eager loading
                if (NavigationProperties != null && NavigationProperties.Count > 0)
                {
                    dbQuery = NavigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                }

                item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(where);
            }

            return item;
        }

        public virtual T GetSingle(Guid id)
        {
            T item;
            using (var context = new PookDbContext())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                // Apply eager loading
                if (NavigationProperties != null && NavigationProperties.Count > 0)
                {
                    dbQuery = NavigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                }

                item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(b => b.Id == id);
            }
            if (item == null)
                throw new NotFoundException($"The provided Id ({id}) is not found in {typeof(T)} table");

            return item;
        }

        public virtual void Add(params T[] items)
        {
            using (var context = new PookDbContext())
            {
                foreach (T item in items)
                {
                    if (item.Id == Guid.Empty)
                    {
                        item.Id = Guid.NewGuid();
                    }

                    item.CreatedOn = DateTime.Now;
                    item.UpdatedOn = DateTime.Now;

                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual void Update(params T[] items)
        {
            using (var context = new PookDbContext())
            {
                foreach (T item in items)
                {
                    item.UpdatedOn = DateTime.Now;

                    T attachedEntity = context.Set<T>().SingleOrDefault(e => e.Id == item.Id);
                    if (attachedEntity != null)
                    {
                        var entry = context.Entry(attachedEntity);
                        entry.CurrentValues.SetValues(item);

                        // Attach user entity and set all properties as modified
                        entry.State = EntityState.Modified;

                        if (IgnoreProperties != null && IgnoreProperties.Count > 0)
                        {
                            foreach (var ignoreProperty in IgnoreProperties)
                            {
                                entry.Property(ignoreProperty).IsModified = false;
                            }
                        }

                        entry.Property(i => i.CreatedOn).IsModified = false;
                        entry.Property(i => i.CreatedBy).IsModified = false;
                    }
                }

                context.SaveChanges();
            }
        }

        public virtual void Delete(Guid id)
        {
            var itemToRemove = GetSingle(i => i.Id == id);
            Delete(itemToRemove);
        }

        public virtual void Delete(params T[] items)
        {
            using (var context = new PookDbContext())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
    }
}
