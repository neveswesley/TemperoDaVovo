namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IUnitOfWork
{
    Task Commit();
}