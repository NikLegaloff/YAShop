using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using YAShop.BusinessLogic.DomainModel;
using YAShop.BusinessLogic.Presistense.MSSQL;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace YAShop.BusinessLogic.Bus
{
    public class CommandExecutor
    {
        private readonly Action<string> _logger;

        public CommandExecutor(Action<string> logger)
        {
            _logger = logger;
        }

        public async Task<bool> TakeOneAndExecute()
        {
            Guid takeId=Guid.NewGuid();
            await MSSqlDb.Execute("update CommandDTO set TakeId='" + takeId + "', StartedDate=GetDate() where id in (select top 1 Id from CommandDTO where takeid is null order by createddate)");
            var list = await Registry.Current.Data.Commands.Select(" where takeid='" + takeId + "'");
            if (list == null || list.Length == 0) return false;
            var commandDTO = list[0];
            string commandName = "unknown";
            try
            {
                var o = JsonConvert.DeserializeObject(commandDTO.BodyJSON,new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
                commandName = o.GetType().Name;
                var command = (ICommand)o;
                var res = await Execute(command, commandDTO.Id);
                await MSSqlDb.Execute("update CommandDTO set state=" + (res? 2:3) + ", EndedDate=GetDate() where id='" + commandDTO.Id + "'");
                _logger(commandName + "\t" + (res ? "success" : "faill"));
            }
            catch (Exception e)
            {
                _logger(commandName + "\t" + "exception\t" + e.Message);
                await MSSqlDb.Execute("update CommandDTO set state=3 and status=@status, EndedDate=GetDate() where id='" + commandDTO.Id + "'", new {status=e.ToString()});
                return true;
            }
            return true;
        }

        async Task<bool> Execute(ICommand command, Guid id)
        {
            var commandType = command.GetType();
            var name = commandType.FullName + "Handler";
            var assembly = Assembly.GetAssembly(GetType());
            var type = assembly.GetType(name, false, true);
            var inst = Activator.CreateInstance(type, args: id);

            MethodInfo method = inst.GetType().GetMethod("Process");
            return await (Task<bool>) method.Invoke(inst, new[] { command });
        }

    }
    public class CommandBus
    {
        public CommandBus()
        {
        }

        public async Task<Guid> Invoke(ICommand command)
        {
            var dto = new CommandDTO();
            dto.BodyJSON = JsonConvert.SerializeObject(command, Formatting.None, new JsonSerializerSettings
            {
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                TypeNameHandling = TypeNameHandling.All
            });
            dto.State=CommandSatet.Ready;
            dto.CreatedDate = DateTime.Now;
            await Registry.Current.Data.Commands.Save(dto);
            return dto.Id;
        }
        
    }
}