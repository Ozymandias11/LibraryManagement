using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
    public class CreateReservationDto
    {
        public Guid CustomerID { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public string? EmployeeId { get; set; }
        public IEnumerable<BookCopyReservationRequest>? BookCopyReservations { get; set; }
    }
}
