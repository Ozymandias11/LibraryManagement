using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   public class ReservationDto
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerID { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public Guid? ReturnCustomerId { get; set; }
        public bool IsLate { get; set; }
        public string? EmployeeId { get; set; }
        public IEnumerable<ReservationItemDto>? ReservationItems { get; set; }
    }
}
