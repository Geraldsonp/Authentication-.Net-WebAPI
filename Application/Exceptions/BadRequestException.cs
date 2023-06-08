namespace Application.Exceptions;

public class BadRequestException : DomainException
{
    public override int StatusCode { get; set; } = 500;

    public BadRequestException(string message): base(message)
    {}
}