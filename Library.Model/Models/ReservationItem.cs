using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class ReservationItem : BaseModel
    {
        public Guid ReservationItemId { get; set; }
        public Guid ReservationId { get; set; }
        public Guid BookCopyID { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public Reservation? Reservation { get; set; }
        public BookCopy? BookCopy { get; set; }
    }
}
