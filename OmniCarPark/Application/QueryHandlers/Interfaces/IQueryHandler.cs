namespace OmniCarPark.Application.QueryHandlers.Interfaces;

public interface IQueryHandler<in TIn, TOut>
{
    public Task<TOut> Handle(TIn command);
}