using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }


    }
}
