﻿using Library.Model.Enums;
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
      int Quantity,
      Guid SelectedBookId,
      Guid SelectedPublisherId,
      Guid SelectedRoomId,
      Guid SelectedShelfId
     )
    {
        public CreateBookCopyDto() : this(0, "", default, 0, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty) { }
    }


}
