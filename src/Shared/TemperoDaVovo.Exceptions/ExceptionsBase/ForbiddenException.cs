namespace TemperoDaVovo.Exceptions.ExceptionsBase;

public class ForbiddenException : TemperoDaVovoException
{
    public IList<string> ErrorMessages { get; set; }

    public ForbiddenException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}