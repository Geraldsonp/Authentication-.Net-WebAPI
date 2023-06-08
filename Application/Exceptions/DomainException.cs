namespace Application.Exceptions;

public class DomainException : Exception
{
    public virtual int StatusCode { get; set; }
    public DomainException(string message) : base(message)
    {
    }
}