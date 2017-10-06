using System;
using System.Collections;
using System.Collections.Generic;
using YAShop.BusinessLogic.Infrastr;

namespace YAShop.ConsoleApp
{
    public class ProgrCommonInfrProvider : ICommonInfrastructureProvider
    {
        public object GetFromSession(string key)
        {
            throw new NotImplementedException();
        }

        public void PutInSession(string key, object subj)
        {
            throw new NotImplementedException();
        }



        public IDictionary IdentityMap
        {
            get
            {
                return new Hashtable();
            }
        }
    }
}