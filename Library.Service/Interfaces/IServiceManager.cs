
using Library.Service.Library.Interfaces;

namespace Library.Service.Interfaces
{
    public interface IServiceManager
    {
       IAuthentificationService AuthenticationService { get; }
       IAuthorService AuthorService { get; }
    }
}
