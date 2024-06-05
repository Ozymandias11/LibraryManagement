using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Publisher : BaseModel
    {
        public Guid PublisherId { get; set; }
        public string? PublisherName { get; set; }  
        public ICollection<BookPublisher>? Books { get; set; }
       

    }
}
