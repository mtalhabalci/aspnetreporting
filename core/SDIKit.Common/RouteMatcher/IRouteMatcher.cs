using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace SDIKit.Common.RouteMatcher
{
    public interface IRouteMatcher
    {
        RouteValueDictionary Match(string routeTemplate, string requestPath, IQueryCollection query = null);
    }
}