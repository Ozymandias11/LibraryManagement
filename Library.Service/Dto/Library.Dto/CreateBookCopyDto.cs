using Library.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   public record CreateBookCopyDto(
       int NumberOfPages,
       string Edition,
       Status Status,
       int Quantity
       );


}
