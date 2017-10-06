using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using YAShop.BusinessLogic.Bus;

namespace YAShop.ConsoleApp
{
    
    public class CommandApiClient
    {
        
        private RestClient _client;

        public CommandApiClient(string apiUrl)
        {
            _client=new RestClient(apiUrl.TrimEnd('/') + "/api");
        }

        private Guid _lastCommainId=Guid.Empty;

        public string ExecuteCommand(string command)
        {
            if (command.StartsWith("state")) return GetState(command);
            if (command == "list") return GetList();
            if (command.StartsWith("run ")) return Run(command.Replace("run ", ""));
            return "Unknown command";
        }

        private string Run(string command)
        {
            var res = CallSubj("command", Method.GET, new Dictionary<string, string>
            {
                {"command",command},
                {"key",CommandBus.BusSecret },
            });
            if (!Guid.TryParse(res.ToString(), out _lastCommainId)) return res;
            return  "[" + _lastCommainId + "] Started...";
        }

        private string GetList()
        {
            dynamic res =  CallSubj("command", Method.GET);
            List<string> list = new List<string>();
            foreach (var str in res)
            {
                list.Add(str.ToString());
            }
            return string.Join("\n", list);
        }

        private string Call(string resource, Method method, Dictionary<string,string> param=null  )
        {
            RestRequest request = new RestRequest(resource, method);
            if (param!=null)
                foreach (var pair in param)
                {
                    request.AddParameter(pair.Key, pair.Value);
                }
            IRestResponse response = _client.Execute(request);
            return response.Content; // raw content as string
        }
        private dynamic CallSubj(string resource, Method method, Dictionary<string, string> param = null)
        {
            var value = Call(resource, method,param);
            return JsonConvert.DeserializeObject(value);
        }

        private string GetState(string command)
        {
            if (command.Contains(" "))
            {
                var ss = command.Split(' ');
                _lastCommainId = Guid.Parse(ss[1]);
            }
            if (_lastCommainId == Guid.Empty) return "No commands";
            var res = CallSubj("command", Method.GET, new Dictionary<string, string>
            {
                {"commandId", _lastCommainId.ToString()},
            });
            return res.ToString();
        }
    }
}
