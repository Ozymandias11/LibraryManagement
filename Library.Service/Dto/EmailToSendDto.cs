using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
    public record EmailToSendDto(
    string Username,
    string Email,
    string resetLink
    );
}
