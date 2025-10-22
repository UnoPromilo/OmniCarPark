namespace OmniCarPark.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task SaveChanges()
    {
        return context.SaveChangesAsync();
    }
}