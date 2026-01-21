namespace TemperoDaVovo.Exceptions.ExceptionsBase;

public class UnauthorizedException : TemperoDaVovoException
{
    public IList<string> ErrorMessages { get; set; }

    public UnauthorizedException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}