using System;
using System.Threading.Tasks;
using YAShop.BusinessLogic.Presistense.MSSQL;
using YAShop.BusinessLogic.Service.Product.Viewing;

namespace YAShop.BusinessLogic.Service.User
{
    public class UserService : AbstractService
    {
        public DomainModel.User Get(string login, string pass)
        {
            throw new NotImplementedException();
        }
    }
}