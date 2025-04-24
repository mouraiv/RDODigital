namespace Infra.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) { }

        protected BaseException(string message, Exception innerException) 
            : base(message, innerException) { }
    }

    public class InfrastructureException : BaseException
    {
        public InfrastructureException(string message) : base(message) { }

        public InfrastructureException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
