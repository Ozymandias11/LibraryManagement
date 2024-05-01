using Microsoft.AspNetCore.Identity;


namespace Library.Model.Models
{
    public class Employee : IdentityUser
    {
      
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
  
        public ICollection<Reservation>? Reservations { get; set; }
     



    }
}
