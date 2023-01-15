using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controllers;

public class ApiKeyAuthAttribute : ActionFilterAttribute
{
    private const string ApiKeyHeaderName = "ApiKey";
    private readonly string _apiKey;

    public ApiKeyAuthAttribute(string apiKey)
    {
        _apiKey = apiKey;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

        if (providedApiKey != _apiKey)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        base.OnActionExecuting(context);
    }
}
