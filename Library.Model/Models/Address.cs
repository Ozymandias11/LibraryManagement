using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Address
    {  
        public required string Street { get; set; }  
        public required string City { get; set; }    
        public required string ZipCode { get; set; }   
    }
}
