// Exceções base
namespace Infra.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) {}
    }

    public class InfrastructureException : BaseException
    {
        public InfrastructureException(string message) : base(message) {}
    }
}