using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   
    public record PublisherDto(
        Guid PublisherId,
        string PublisherName,
        string PhoneNumber,
        string Email
        );
    
    
}
