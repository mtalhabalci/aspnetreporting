using SDIKit.Common.Enums;
using SDIKit.Common.Helpers;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System;
using System.Collections.Generic;

namespace SDIKit.Common.Types.Result
{
    public abstract class BaseResult
    {
        public const string DefaultErrorMessage = "İşlem sırasında bir hata oluştu!";
        public const string DefaultFormInvalid = "Parametreleri kontrol ediniz";
        public const string DefaultSuccessMessage = "İşlem tamamlandı";

        public BaseResult(string title, string content, ResultState resultState = SDIKit.Common.Enums.ResultState.Success, List<ModelValidationResult> validations = null)
        {
            Id = Guid.NewGuid().ToString();
            if (String.IsNullOrWhiteSpace(title))
            {
                switch (resultState)
                {
                    case SDIKit.Common.Enums.ResultState.Error:
                        title = "Hata!";
                        break;

                    case SDIKit.Common.Enums.ResultState.Info:
                        title = "Bilgilendirme";
                        break;

                    case SDIKit.Common.Enums.ResultState.Success:
                        title = "Bilgilendirme";
                        break;

                    case SDIKit.Common.Enums.ResultState.Warning:
                        title = "Uyarı!";
                        break;
                }
            }

            Title = title;
            Content = content;
            Validations = validations ?? new List<ModelValidationResult>();
            State = resultState;
            Properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public string Content { get; set; }
        public string Id { get; set; }

        public bool IsValid
        {
            get
            {
                if (State != SDIKit.Common.Enums.ResultState.Success || Validations.Count > 0)
                    return false;

                return true;
            }
        }

        public IDictionary<string, object> Properties { get; set; }

        public string ResultState
        {
            get
            {
                return State.GetDisplayName();
            }
        }

        public ResultState State { get; set; }
        public string Title { get; set; }
        public List<ModelValidationResult> Validations { get; set; }
    }
}