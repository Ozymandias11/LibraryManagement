using Library.Data.NewFolder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
    
        private readonly RepositoryContext _repositoryContext;
        protected RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }


        public void Create(T entity) => _repositoryContext.Set<T>().Add(entity);
        
            
        
        public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);



        // trackChanges hepls us to speed up read only queries
        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _repositoryContext.Set<T>().AsNoTracking() :
            _repositoryContext.Set<T>();

      

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges) =>
             !trackChanges ?
            _repositoryContext.Set<T>().AsNoTracking().Where(expression) :
            _repositoryContext.Set<T>().Where(expression);

        public void Update(T entity) => _repositoryContext.Set<T>().Update(entity); 
       
    }
}
