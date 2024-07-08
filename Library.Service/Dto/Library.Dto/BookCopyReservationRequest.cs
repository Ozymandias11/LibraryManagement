using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
    public class BookCopyReservationRequest
    {
        public Guid OriginalBookId { get; set; }
        public string? Edition { get; set; }
        public Guid PublisherId { get; set; }
        public int Quantity { get; set; }
    }
}
