using System;
using YAShop.BusinessLogic.Infrastr;
using YAShop.BusinessLogic.Presistense;
using YAShop.BusinessLogic.Service;

namespace YAShop.BusinessLogic
{
    public class Registry
    {
        private static Registry _current;
        public static Registry Current
        {
            get
            {
                if (_current == null) throw new Exception("Not initialized");
                return _current;
            }
        }

        public static void Init(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            _current = new Registry
            {
                Services = Services.Create(),
                Infrastructure = new Infrastructure(commonInfrastructureProvider),
                Data = new DataProviders()
            };
        }

        public Infrastructure Infrastructure { get; set; }
        public Services Services { get; set; }
        public DataProviders Data { get; set; }
    }
}