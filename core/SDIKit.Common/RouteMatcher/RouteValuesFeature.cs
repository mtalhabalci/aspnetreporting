using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;

namespace SDIKit.Common.RouteMatcher
{
    public class RouteValuesFeature : IRouteValuesFeature
    {
        public RouteValueDictionary RouteValues { get; set; }

        public RouteValuesFeature(RouteValueDictionary routeValues)
        {
            RouteValues = routeValues;
        }
    }
}