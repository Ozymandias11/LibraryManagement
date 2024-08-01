using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
    public record CreateBookCopyLogDto(
       Guid LogId,
       Guid OriginalBookId,
       Guid PublisherId,
       string State,
       string Message,
       int QuantityModified,
       DateTime TimeStamp
       );
}
