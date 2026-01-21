namespace TemperoDaVovo.Exceptions.ExceptionsBase;

public class BusinessException : TemperoDaVovoException
{
    public IList<string> ErrorMessages { get; set; }

    public BusinessException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}