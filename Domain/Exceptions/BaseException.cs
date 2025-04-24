namespace Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) { }

        protected BaseException(string message, Exception innerException) 
            : base(message, innerException) { }
    }

    public class BusinessException : BaseException
    {
        public BusinessException (string message) : base(message) { }

        public BusinessException (string message, Exception innerException)
            : base(message, innerException) { }
    }
}
