using System;
using System.Collections.Generic;
using System.Reflection;

namespace YAShop.BusinessLogic.Bus
{
    public class CommandBus
    {
        public CommandBus()
        {
        }

        readonly Dictionary<Type,object> _handlers = new Dictionary<Type, object>();
        public void Execute(ICommand command)
        {
            object inst;
            var commandType = command.GetType();

            lock (_handlers)
            {
                if (!_handlers.ContainsKey(commandType)) { 
                    var name = commandType.FullName+ "Handler";
                    var assembly = Assembly.GetAssembly(GetType());
                    var type = assembly.GetType(name,false,true);
                    inst = Activator.CreateInstance(type, args:this);
                    _handlers.Add(commandType, inst);
                }
                else
                    inst =_handlers[commandType];
            }
            MethodInfo method = inst.GetType().GetMethod("Process");
            method.Invoke(inst, new []{command});
        }
    }
}