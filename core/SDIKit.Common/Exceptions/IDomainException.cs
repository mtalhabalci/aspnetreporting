using System;

namespace SDIKit.Common.Exceptions
{
    /// <summary>
    /// Implementing Domain Operation Exceptions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDomainException<T>
    {
        Guid Code { get; set; }
        T Type { get; set; }
        string CustomParameter { get; set; }
        public ExceptionLevel ExceptionLevel { get; set; }
    }
}