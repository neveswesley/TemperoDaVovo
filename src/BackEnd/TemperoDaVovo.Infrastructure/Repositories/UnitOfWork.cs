using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}