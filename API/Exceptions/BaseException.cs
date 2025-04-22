// Exceções base
namespace API.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) {}
    }

}