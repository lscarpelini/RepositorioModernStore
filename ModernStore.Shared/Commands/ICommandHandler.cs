namespace ModernStore.Shared.Commands
{
    //Pode ser de qualquer tipo que herde de ICommadResult
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handle(T Command);
    }
}
