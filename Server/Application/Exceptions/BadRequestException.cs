namespace Application.Exceptions;

public class BadRequestException : DomainException
{
    public override int StatusCode { get; set; } = 500;

    public BadRequestException(IEnumerable<string> message): base(string.Join(", ", message))
    {

    }
    public BadRequestException(string message): base(message)
    {

    }
}