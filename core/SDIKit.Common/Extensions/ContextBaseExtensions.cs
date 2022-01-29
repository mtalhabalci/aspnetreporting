using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;

using System.Linq;

namespace SDIKit.Common.Extensions
{
    public static class ContextBaseExtensions
    {
        public static string GetIp(this HttpContext context)
        {
            return context?.Features?.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
        }

        public static string GetRemoteIp(this HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-FORWARDED-FOR", out StringValues values))
            {
                var parsedIp = values.ToString();
                return parsedIp.Split(',').First();
            }

            return context.GetIp();
        }

        public static bool IsAjaxRequest(this HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(ApplicationVariables.HttpRequest.RequestedWithHeader, out StringValues values))
            {
                var contextHeadervalue = values.ToString();
                if (contextHeadervalue == ApplicationVariables.HttpRequest.XmlHttpRequest)
                {
                    return true;
                }
            }

            return false;
        }

        public static string UserAgent(this HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(ApplicationVariables.HttpRequest.UserAgent, out StringValues values))
            {
                return values.ToString();
            }

            return string.Empty;
        }
    }
}