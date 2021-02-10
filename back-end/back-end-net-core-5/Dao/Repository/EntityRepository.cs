using System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using back_end_net_core_5.Dao;
using back_end_net_core_5.Utils.Pagination;

namespace back_end_net_core_5.Dao.Repository
{
    public class EntityRepository<T> : IRepository<T>
         where T : class
    {
        public Context Context { get; private set; }

        protected DbSet<T> Set => Context.Set<T>();

        public EntityRepository(Context db_dbContext)
        {
            Context = db_dbContext;
        }

        public IQueryable<T> Query => Set;

        public void Salvar(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                Set.Add(entity);
        }

        public void Update(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                Set.Attach(entity);
            entry.State = EntityState.Modified;

        }

        public void Apagar(T entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
                Set.Attach(entity);
            Set.Remove(entity);
        }

        public T Selecionar(Expression<Func<T, bool>> predicate)
        {
            return Context
                    .Set<T>()
                    .AsNoTracking()
                    .SingleOrDefault(predicate);
        }

        public List<T> Listar(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query)
                    .AsNoTracking()
                    .ToList();
            }
            else
            {
                return query
                    .AsNoTracking()
                    .ToList();
            }
        }

        public PagedItems<T> ListarPaginado(Expression<Func<T, bool>> predicate, PagedOptions pagedFilter)
        {
            if (string.IsNullOrEmpty(pagedFilter.Sort) && pagedFilter.SortManny == null)
            {
                var props = typeof(T)
                    .GetProperties()
                    .Where(prop =>
                        Attribute.IsDefined(prop,
                            typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));

                pagedFilter.Sort = props.First().Name;
            }

            PagedItems<T> paged = new PagedItems<T>();

            var query = Context
                .Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .AsQueryable();

            paged.Total = query.Count();

            if (!string.IsNullOrEmpty(pagedFilter.Sort))
            {
                query = LinqExtension.OrderBy(query, pagedFilter.Sort, pagedFilter.Reverse);
            }
            else
            {
                if (pagedFilter.SortManny != null)
                {
                    var list = pagedFilter.SortManny.ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == 0)
                        {
                            query = LinqExtension.OrderBy(query, list[i].Sort, list[i].Reverse);
                        }
                        else
                        {
                            query = LinqExtension.ThenBy(query, list[i].Sort, list[i].Reverse);
                        }
                    }
                }
            }

            var skip = (pagedFilter.Page.Value * pagedFilter.Size.Value) - pagedFilter.Size.Value;
            query = query.Skip(skip);
            query = query.Take(pagedFilter.Size.Value);

            paged.Items = query.ToList();
            return paged;
        }

    }
}
