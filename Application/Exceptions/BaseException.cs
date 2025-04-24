namespace Application.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) { }

        protected BaseException(string message, Exception innerException) 
            : base(message, innerException) { }
    }

    public class AppException : BaseException
    {
        public AppException(string message) : base(message) { }

        public AppException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class ConflictException : BaseException
    {
        public ConflictException(string message) : base(message) { }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
