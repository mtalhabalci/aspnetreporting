using SDIKit.Common.Extensions;
using SDIKit.Common.Helpers;
using SDIKit.Common.Infrastructure;
using SDIKit.Common.Types;
using SDIKit.Common.Types.Files;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SDIKit.Common.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class TypesController : SharedBaseController
    {
        [HttpGet("{typeName}")]
        public IActionResult Get(string typeName)
        {
            var type = Type.GetType($"{typeName.FromBase64()}");
            var properties = type.GetProperties();
            var definition = new ApplicationTypeDefinition();
            foreach (var property in properties)
            {
                var built = definition.CreateParameter(property.Name);
                var propType = property.PropertyType.Name.ToLower(CultureInfo.GetCultureInfo(1033));//EN
                if (property.PropertyType.IsGenericType)
                {
                    built.MarkAsNullable();
                    if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        if (property.PropertyType.GenericTypeArguments.First().FullName == typeof(FileRequestDto).FullName)
                        {
                            propType = "file";
                        }
                        else
                        {
                            propType = "Array";
                        }
                    }
                    else
                    {
                        propType = property.PropertyType.GenericTypeArguments.First().Name.ToLower(CultureInfo.GetCultureInfo(1033));//EN
                    }
                }

                if (!built.IsNullable)
                {
                    built.Value = Type.GetType(property.PropertyType.FullName)?.GetDefaultValue();
                }

                built = built.SetType(propType).AddValidations(property.GetCustomAttributes());

                definition.AddDefinition(built);
            }
            return Ok(definition);
        }
    }
}