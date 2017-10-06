using System;
using System.Threading;
using System.Threading.Tasks;

namespace YAShop.BusinessLogic.Bus.Commands
{
    
    public class SleepCommand : ICommand
    {
        public int DelayInSec { get; set; }
        
        public SleepCommand(int delayInSec)
        {
            DelayInSec = delayInSec;
        }

        public SleepCommand() {}
    }
    
    public class SleepCommandHandler : AbstractCommandHandler<SleepCommand>
    {
        public SleepCommandHandler(Guid dtoId) : base(dtoId)
        {
        }

        public override async Task<bool> Process(SleepCommand command)
        {
            for (int i = 1; i <= command.DelayInSec; i++)
            {
                Thread.Sleep(1000); 
                await Status(i + " seconds sleep"); 
            }
            return true;

        }
    }
}