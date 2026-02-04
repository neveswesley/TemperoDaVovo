namespace TemperoDaVovo.Exceptions.ExceptionsBase;

public class NotFoundException : TemperoDaVovoException
{
    public IList<string> ErrorMessages { get; set; }

    public NotFoundException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}