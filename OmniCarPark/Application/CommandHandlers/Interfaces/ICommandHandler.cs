namespace OmniCarPark.Application.CommandHandlers.Interfaces;

public interface ICommandHandler<in TIn, TOut>
{
    public Task<TOut> Handle(TIn command);
}