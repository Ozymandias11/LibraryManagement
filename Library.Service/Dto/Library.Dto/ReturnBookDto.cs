using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
    public record ReturnBookDto
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public List<ReturnActionDto>? returnItems { get; set; }
    }
}
