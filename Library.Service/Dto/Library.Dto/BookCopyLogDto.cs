using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
    public record BookCopyLogDto(
        Guid LogId,
        string State,
        string Message,
        int QuantityModified,
        DateTime TimeStamp
        );
    
    
}
