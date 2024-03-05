using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Controllers;

public sealed class ApiFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("Calling Endpoint : ");
        Console.WriteLine(context.HttpContext.Request.Path);
        await next();
    }
}