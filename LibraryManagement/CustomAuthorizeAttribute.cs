using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Web.Mvc;

public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{

    private readonly string[] _roles;

    public CustomAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        // Check if the user is authenticated
        if (!user.Identity.IsAuthenticated)
        {
            RedirectToLoginPage(context);
            return;
        }

        // Check if the user has any of the required roles
        var hasClaims = _roles.Any(role => user.IsInRole(role));
        if (!hasClaims)
        {
            RedirectToAccessDeniedPage(context);
            return;
        }

        await Task.CompletedTask;
    }

    private void RedirectToLoginPage(AuthorizationFilterContext context)
    {
        var returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
        context.Result = new RedirectToActionResult("Login", "Account", new { ReturnUrl = returnUrl });
    }

    private void RedirectToAccessDeniedPage(AuthorizationFilterContext context)
    {
        // to get the URL we tried to access an unautorized page from

        var requestedUrl = context.HttpContext.Request.GetDisplayUrl();
        context.Result = new RedirectToActionResult("AccessDenied", "Account", new { returnUrl = requestedUrl });
    }
}