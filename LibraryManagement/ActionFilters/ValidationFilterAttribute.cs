using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace LibraryManagement.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public ValidationFilterAttribute()
        {
            
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var action = context.RouteData.Values["Action"];
            var controller = context.RouteData.Values["controller"];


            var viewModel = context.ActionArguments.FirstOrDefault(
                x => x.Value.ToString().Contains("ViewModel")).Value;
                


            if(!context.ModelState.IsValid)
            {
                context.Result = new ViewResult
                {
                    ViewName = action?.ToString(),
                    ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                    {
                        Model = viewModel,
                    }
                };
            }
        }
    }
}
