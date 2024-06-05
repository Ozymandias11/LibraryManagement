using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Customer : BaseModel
    {
        public Guid CustomerId { get; set; }
        public string? FirstName { get; set; }  
        public string? LastName { get; set;}
        public string? Address { get; set; } 
        public string? Email {  get; set; }
        public string? PhoneNumber { get; set;}

        public ICollection<Reservation>? Reservations { get; set; }
   

    }
}
