using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
  public record ShelfDto(

      Guid ShelfId,
      int MaxCapacity,
      int ShelfNumber

      );
    
    
}
