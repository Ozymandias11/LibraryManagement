using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   public record CreateBookDto(
       string Title,
       DateTime PublishedYear,
       int Edition,
       IEnumerable<Guid> SelectedAuthorIds,
       IEnumerable<Guid> SelectedPublisherIds,
       IEnumerable<Guid> SelectedCategoryIds
       );
    
    
}
