// Exceções base
namespace Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) {}
    }

    public class DomainException : BaseException
    {
        public DomainException(string message) : base(message) {}
    }

}