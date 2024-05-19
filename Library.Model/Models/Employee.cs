using Microsoft.AspNetCore.Identity;


namespace Library.Model.Models
{
    public class Employee : IdentityUser
    {
      
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public DateTime DateOfBirth { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
     



    }
}
