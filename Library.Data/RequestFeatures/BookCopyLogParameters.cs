using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.RequestFeatures
{
    public class BookCopyLogParameters : RequestParameters
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
    }
}
