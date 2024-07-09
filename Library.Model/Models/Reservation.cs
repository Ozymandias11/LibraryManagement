using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Reservation : BaseModel
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerID { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public Guid? ReturnCustomerId { get; set; }
        public bool IsLate => SupposedReturnDate < DateTime.Now && ActualReturnDate == null;
        public string? EmployeeId { get; set; }
        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<ReservationItem>? ReservationItems { get; set; }


    }
}
