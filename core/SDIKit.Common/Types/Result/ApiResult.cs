using SDIKit.Common.Enums;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.Collections.Generic;

namespace SDIKit.Common.Types.Result
{
    public class ApiResult : BaseResult
    {
        public ApiResult(string title = "Bilgilendirme", string content = DefaultSuccessMessage, ResultState resultState = SDIKit.Common.Enums.ResultState.Success, List<ModelValidationResult> validations = null) : base(title, content, resultState, validations)
        {
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Result { get; set; }

        public ApiResult(T result = default(T), string title = "Bilgilendirme", string content = DefaultSuccessMessage, ResultState resultState = SDIKit.Common.Enums.ResultState.Success, List<ModelValidationResult> validations = null) : base(title, content, resultState)
        {
            Result = result;
        }
    }
}