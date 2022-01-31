using SDIKit.Common.Enums;

namespace SDIKit.Common.Types.Result
{
    public class SavedApiResult<P> : ApiResult<bool>
    {
        public SavedApiResult(bool result, P parameter, string content = "", ResultState state = SDIKit.Common.Enums.ResultState.Success) : base(result, content, resultState: state)
        {
            Parameter = parameter;
        }

        public P Parameter { get; }
    }
}