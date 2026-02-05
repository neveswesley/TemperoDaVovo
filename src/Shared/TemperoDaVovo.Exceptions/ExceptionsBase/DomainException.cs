namespace TemperoDaVovo.Exceptions.ExceptionsBase;

public class DomainException : TemperoDaVovoException
{
    public IList<string> ErrorMessages { get; set; }

    public DomainException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}