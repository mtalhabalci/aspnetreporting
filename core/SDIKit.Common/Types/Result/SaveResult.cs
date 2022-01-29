namespace SDIKit.Common.Types.Result
{
    public class SaveResult<T>
    {
        public SaveResult(bool result, T parameter)
        {
            Result = result;
            Parameter = parameter;
        }

        public bool Result { get; set; }
        public T Parameter { get; set; }
        public string Message { get; set; }
    }
}