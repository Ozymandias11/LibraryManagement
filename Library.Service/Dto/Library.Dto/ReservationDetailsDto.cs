using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
    public class ReservationDetailsDto
    {
        public Guid ReservationId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? EmployeeFullName { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public bool IsLate { get; set; }
        public List<ReservationItemForDetailsDto>? ReservationItems { get; set; }    
        
    }
}
