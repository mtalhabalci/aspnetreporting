using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Primitives;

namespace SDIKit.Common.Infrastructure
{
    public class DefaultValueFromQueryActionConstraint : IActionConstraint
    {
        private readonly string _parameter;

        public DefaultValueFromQueryActionConstraint(string parameter)
        {
            _parameter = parameter;
        }

        public int Order => 999;

        public bool Accept(ActionConstraintContext context)
        {
            if (context.RouteContext.HttpContext.Request.Query.TryGetValue(_parameter, out StringValues value))
            {
                foreach (var item in value)
                {
                    if (item == string.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}