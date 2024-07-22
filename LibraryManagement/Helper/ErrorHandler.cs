using FluentResults;
using Library.Service.Logging;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Helper
{
    public static class ErrorHandler
    {
        public static IActionResult HandleFailure<T>(this Controller controller, Result result, T viewModel, ILoggerManager loggerManager, string actionName, string errorContext)
        {
            if (result.IsFailed)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Message ?? $"An error occurred while {errorContext}";
                loggerManager.LogError($"An error occurred while {errorContext}: {errorMessage}");
                controller.ModelState.AddModelError("", errorMessage);
                return controller.View(viewModel);
            }

            return controller.RedirectToAction(actionName);
        }
    }
}
