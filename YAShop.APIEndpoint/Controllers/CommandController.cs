using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Bus;

namespace YAShop.APIEndpoint.Controllers
{
    public class CommandController : ApiController
    {
        // GET api/Command - получить список доступных команд
        public IEnumerable<string> Get()
        {
            var type = typeof(ICommand);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)).ToList();
            types = types.Where(t => t.IsClass).ToList();
            return
                types.Select(
                    t =>
                        t.Name + " (" +
                        string.Join(", ",
                        (t.GetConstructors().FirstOrDefault(c => c.GetParameters().Length > 0) ??
                         t.GetConstructors().First()).GetParameters().Select(p => p.Name)) + ")");
        }

        // GET api/Command/5 - получить статус
        public async Task<string> Get(Guid commandId)
        {
            var command = await Registry.Current.Data.Commands.Find(commandId);
            return command.State + ": " + command.Status;
        }

        // POST api/Command - запустить команду
        public async Task<string> Get(string command, string key)
        {
            if (key != CommandBus.BusSecret) return "Wrong key";

            try
            {
                var name = command.Split('(')[0].Trim();
                var param = command.Contains("(")
                    ? command.TrimEnd(')').Split('(')[1].Split(',').Select(s => s.Trim()).ToList()
                    : null;
                var fullName = "YAShop.BusinessLogic.Bus.Commands." + name;
                var t = typeof(ICommand).Assembly.GetType(fullName);
                var forContructor = new List<object>();

                var pp =
                (t.GetConstructors().FirstOrDefault(c => c.GetParameters().Length > 0) ??
                 t.GetConstructors().First()).GetParameters();
                if (param != null)
                {
                    var i = 0;
                    foreach (var p in pp)
                    {
                        var paramStr = param[i++];
                        if (p.ParameterType == typeof(string)) forContructor.Add(paramStr);
                        else if (p.ParameterType == typeof(Guid)) forContructor.Add(Guid.Parse(paramStr));
                        else if (p.ParameterType == typeof(int)) forContructor.Add(int.Parse(paramStr));
                        else if (p.ParameterType == typeof(DateTime)) forContructor.Add(DateTime.Parse(paramStr));
                    }
                }
                ICommand cmd = null;
                if (forContructor.Count == 0) cmd = (ICommand) Activator.CreateInstance(t);
                else if (forContructor.Count == 1) cmd = (ICommand) Activator.CreateInstance(t, forContructor[0]);
                else if (forContructor.Count == 2)
                    cmd = (ICommand) Activator.CreateInstance(t, forContructor[0], forContructor[1]);
                else if (forContructor.Count == 3)
                    cmd = (ICommand) Activator.CreateInstance(t, forContructor[0], forContructor[1], forContructor[2]);
                else if (forContructor.Count == 4)
                    cmd =
                        (ICommand)
                        Activator.CreateInstance(t, forContructor[0], forContructor[1], forContructor[2],
                            forContructor[3]);
                if (cmd != null)
                {
                    var guid = await Registry.Current.Bus.Invoke(cmd);
                    return guid.ToString();
                }
                return "Unknown command " + command;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        // PUT api/Command/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Command/5
        public void Delete(int id)
        {
        }
    }
}