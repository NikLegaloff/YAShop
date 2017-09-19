namespace YAShop.BusinessLogic.Bus
{
    public abstract class AbstractCommandHandler<T> where T : ICommand
    {
        protected CommandBus Bus { get; }

        protected AbstractCommandHandler(CommandBus bus)
        {
            Bus = bus;
        }

        public abstract void Process(T command);
    }
}