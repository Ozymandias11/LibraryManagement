using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class CategoryTitle : BaseModel
    {
        public Guid CatgeoryTitleId { get; set; }   
        public Guid CategoryId { get; set; }    
        public string? Title {  get; set; }

        public string? Language { get; set; }

        public Category? Category { get; set; }

    }
}
