using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
    public interface IResultHandlerService
    {
        void HandleResult<T>(Result<T> result, string actionDescription);
        void HandleResult(Result result, string actionDescription);
    }
}
