namespace Application.Exceptions;

public class NotFoundException : DomainException
{
    public override int StatusCode { get; set; } = 404;

    public NotFoundException(string entityName) : base($"{entityName} was not found.")
    {
        
    }
}