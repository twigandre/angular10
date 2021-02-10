using System;
using System.Collections.Generic;
using System.Linq;
using back_end_net_core_5.Dao;
using System.Linq.Expressions;

namespace back_end_net_core_5.Dao.Repository
{
    public interface IRepository<T> 
        where T : class
    {
        void Salvar(T entity);

        void Update(T entity);

        void Apagar(T entity);

        public List<T> Listar(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        public T Selecionar(Expression<Func<T, bool>> predicate);

        IQueryable<T> Query { get; }

        Context Context { get; }
    }
}
