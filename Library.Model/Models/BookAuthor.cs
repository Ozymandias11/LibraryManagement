using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookAuthor
    {

       
        public Guid BookId { get; set; }
        public Guid AuthorID { get; set; }


        public Book? Book { get; set; }
        public Author? Author { get; set; } 
    }
}
