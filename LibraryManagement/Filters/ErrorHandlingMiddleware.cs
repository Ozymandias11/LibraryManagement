using System.Web.Mvc;

namespace LibraryManagement.Filters
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException)
            {
                context.Response.Redirect("Views/AccessDenied.cshtml/");
            }
        }
    }

}
