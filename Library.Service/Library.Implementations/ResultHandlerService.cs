using FluentResults;
using Library.Service.Library.Interfaces;
using Library.Service.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class ResultHandlerService : IResultHandlerService
    {
        private readonly ILoggerManager _loggerManager;

        public ResultHandlerService(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void HandleResult<T>(Result<T> result, string actionDescription)
        {
            if (result.IsFailed)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Message ?? $"An error occurred while {actionDescription}";
                _loggerManager.LogError($"An error occurred while {actionDescription}: {errorMessage}");
            }
        }
        public void HandleResult(Result result, string actionDescription)
        {
            if (result.IsFailed)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Message ?? $"An error occurred while {actionDescription}";
                _loggerManager.LogError($"An error occurred while {actionDescription}: {errorMessage}");
            }
        }
    }
}
