using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.RequestFeatures
{
    public class BookParameters : RequestParameters
    {
        public BookParameters() => OrderBy = "CreatedDate";    
        
    }
}
