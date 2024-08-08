using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Helpers
{
   public class LostBooksReport
    {
        public required string Name { get; set; }   
        public int LostBooksCount { get; set; }
    }
}
