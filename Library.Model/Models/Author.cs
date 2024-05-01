using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Author
    {

        public Guid AuthorId { get; set; }
        public string? FirstName { get; set; }   
        public string? LastName { get; set; }   
        public DateTime DateOfBirth { get; set; }
        public ICollection<BookAuthor>? BooksWritten{ get; set; }

    }
}
