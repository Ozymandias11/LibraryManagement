using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   public record ModifyBookCopiesDto(
         Guid OriginalBookId, 
         Guid PublisherId,
         string Edition,
         string State,
         int QuantityModified,
         string Message,
         Guid RoomId,
         Guid ShelfId
       );
}
