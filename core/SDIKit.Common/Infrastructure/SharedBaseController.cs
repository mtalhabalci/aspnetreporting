using SDIKit.Common.Enums;
using SDIKit.Common.Types.Result;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace SDIKit.Common.Infrastructure
{
    [Route("[controller]")]
    public class SharedBaseController : ControllerBase
    {
        public IServiceProvider Resolver => HttpContext?.RequestServices;

        protected T GetService<T>()
        {
            return Resolver.GetService<T>();
        }

        /// <summary>
        /// if "data" object is null, returned to successfully ApiResult instance
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected ObjectResult ApiResult(object data = null, ResultState state = ResultState.Success)
        {
            if (data == null)
                data = new ApiResult(resultState: state);

            var result = new ObjectResult(data);

            if (data is ApiResult)
            {
                var apiResult = data as ApiResult;
                result.StatusCode = apiResult.State == ResultState.Success ?
                                        StatusCodes.Status200OK :
                                        apiResult.State == ResultState.Warning ?
                                            StatusCodes.Status202Accepted :
                                            apiResult.State == ResultState.Info ?
                                                StatusCodes.Status201Created :
                                                StatusCodes.Status400BadRequest;
            }

            return result;
        }
    }
}