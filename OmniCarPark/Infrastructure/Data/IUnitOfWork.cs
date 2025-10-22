namespace OmniCarPark.Infrastructure.Data;

public interface IUnitOfWork
{
    Task SaveChanges();
}