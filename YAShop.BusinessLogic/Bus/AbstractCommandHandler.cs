using System;
using System.Threading.Tasks;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.Bus
{
    public interface ICommandExecutionEnv
    {
    }

    public abstract class AbstractCommandHandler<T> where T : ICommand
    {
        protected AbstractCommandHandler(Guid dtoId)
        {
            DTOId = dtoId;
        }

        private Guid DTOId { get; }

        protected async Task Status(string status)
        {
            await MSSqlDb.Execute("update CommandDTO set status=@status where id=@id", new {status, id = DTOId});
        }

        public abstract Task<bool> Process(T command);
    }
}