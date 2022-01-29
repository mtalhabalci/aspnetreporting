using SDIKit.Common.Attributes;
using SDIKit.Common.Helpers;
using SDIKit.Common.Infrastructure;
using SDIKit.Common.Types;
using SDIKit.Common.Types.Result;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SDIKit.Common.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class EnumsController : SharedBaseController
    {
        [HttpGet("{typeName}")]
        public async Task<IActionResult> Get(string typeName)
        {
            var assemblyName = typeName.IndexOf("Types.Enums") > -1 ? typeName.Substring(0, typeName.IndexOf(".Types.Enums")) : null;
            var assembly = Assembly.Load(assemblyName);
            var type = assembly.GetType(typeName);

            List<dynamic> list = new List<dynamic>();

            foreach (Enum value in Enum.GetValues(type))
            {
                list.Add(new
                {
                    Value = value.GetHashCode(),
                    Text = value.GetDisplayName()
                });
            }
            var result = new ApiResult<List<dynamic>>(list);
            return ApiResult(await Task.FromResult(result));
        }

        [HttpGet("attributes/{typeName}")]
        public async Task<IActionResult> GetAttributes(string typeName)
        {
            Type type = Type.GetType(typeName);
            List<dynamic> list = new List<dynamic>();

            foreach (Enum value in Enum.GetValues(type))
            {
                FieldInfo fieldInfo = type.GetField(value.ToString());
                DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                UnitAttribute[] unitAttributes = (UnitAttribute[])fieldInfo.GetCustomAttributes(typeof(UnitAttribute), false);

                list.Add(new
                {
                    Unit = unitAttributes.Length == 0 ? "" : unitAttributes[0].Unit,
                    Value = value.GetHashCode(),
                    Description = descriptionAttributes.Length == 0 ? "" : descriptionAttributes[0].Description
                });
            }

            var result = new ApiResult<List<dynamic>>(list);
            return ApiResult(await Task.FromResult(result));
        }

        [HttpGet("load/{assemblyName}")]
        public async Task<IActionResult> Load(string assemblyName)
        {
            var list = Assembly.Load(assemblyName)
                               .GetTypes()
                               .Where(t => t.IsEnum && t.IsPublic && t.FullName.StartsWith($"{assemblyName}.Types.Enums"))
                               .Select(i => new EnumModel(i))
                               .ToList();

            var result = new ApiResult<List<EnumModel>>(list);
            return ApiResult(await Task.FromResult(result));
        }
    }
}