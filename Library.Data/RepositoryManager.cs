using Library.Data.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{

    // Repository Manager is usefull for grouping multiple operations into a signe unit of work
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
       

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
       
    }
}
