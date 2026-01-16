namespace TemperoDaVovo.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : TemperoDaVovoException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}