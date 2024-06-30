using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Errors.NotFoundError
{
    public class NotFoundError : Error
    {
        public NotFoundError(string entityName, object entityId)
            : base($"{entityName} with id {entityId} not found") { }
    }
}
