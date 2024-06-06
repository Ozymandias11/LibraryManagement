using Library.Data.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.NewFolder
{
    public interface IRepositoryManager
    {
        IAuthorRepository AuthorRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        Task SaveAsync();
    }
}
