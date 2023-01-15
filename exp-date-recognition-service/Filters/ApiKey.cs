using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exp.Date.Recognition.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
  private const string API_KEY_HEADER_NAME = "X-API-Key";

  public void OnAuthorization(AuthorizationFilterContext context)
  {
    var submittedApiKey = GetSubmittedApiKey(context.HttpContext);
    var apiKey = GetApiKey(context.HttpContext);

    if (!IsApiKeyValid(apiKey, submittedApiKey))
    {
      context.Result = new UnauthorizedResult();
    }
  }

  private static string GetSubmittedApiKey(HttpContext context)
  {
    return context.Request.Headers[API_KEY_HEADER_NAME];
  }

  private static string GetApiKey(HttpContext context)
  {
    var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
    return appSettings.GetValue<string>("API_KEY");
  }

  private static bool IsApiKeyValid(string apiKey, string submittedApiKey)
  {
    if (string.IsNullOrEmpty(submittedApiKey)) return false;

    var apiKeySpan = MemoryMarshal.Cast<char, byte>(apiKey.AsSpan());
    var submittedApiKeySpan = MemoryMarshal.Cast<char, byte>(submittedApiKey.AsSpan());

    return CryptographicOperations.FixedTimeEquals(apiKeySpan, submittedApiKeySpan);
  }
}