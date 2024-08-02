using Library.Data.NewFolder;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
    {
    
        private readonly RepositoryContext _repositoryContext;
        protected RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }


        public void Create(T entity)
        {
         
            entity.CreatedDate = DateTime.Now;
            _repositoryContext.Set<T>().Add(entity);
        }
        
            
        
        public void Delete(T entity)
        {

            if(IsJunctionTable(typeof(T)))
            {
                _repositoryContext.Set<T>().Remove(entity);
            }
            else
            {
                entity.DeletedDate = DateTime.Now;
                _repositoryContext.Set<T>().Update(entity);
            }


        }
        // trackChanges helps us to speed up read-only queries
        // and do connected updates
        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _repositoryContext.Set<T>().AsNoTracking() :
            _repositoryContext.Set<T>();

      

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges) =>
             !trackChanges ?
            _repositoryContext.Set<T>().AsNoTracking().Where(expression) :
            _repositoryContext.Set<T>().Where(expression);

        public void Update(T entity)
        {
           entity.UpdatedDate = DateTime.Now;
            _repositoryContext.Set<T>().Update(entity);
        }

        public void DetachEntities(IEnumerable<T> entities)
        {
            _repositoryContext.DetachEntries(entities);
        }


        //
        private bool IsJunctionTable(Type entityType)
        {
            return entityType.Name.EndsWith("BookAuthor") ||
                entityType.Name.EndsWith("BookPublisher") ||
                entityType.Name.EndsWith("BookCategory");
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _repositoryContext.Set<T>().AddRange(entities);
        }
    }
}
