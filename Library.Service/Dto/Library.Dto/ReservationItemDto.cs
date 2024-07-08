using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   public class ReservationItemDto
    {
        public Guid ReservationItemId { get; set; }
        public Guid BookCopyID { get; set; }
        public DateTime? ActualReturnDate { get; set; }
  
        public string? BookTitle { get; set; }
 
    }
}
