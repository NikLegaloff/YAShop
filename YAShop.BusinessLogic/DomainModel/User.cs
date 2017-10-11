using System.Data;
using YAShop.BusinessLogic.Presistense;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.DomainModel
{
    public enum UserRole
    {
        Superuser,Admin, User
    }
    public class User : DomainObject
    {
        [DBField(SqlDbType.NVarChar, 128)]
        public string Email { get; set; }
        [DBField(SqlDbType.NVarChar, 128)]
        public string Password{ get; set; }
        [DBField(SqlDbType.Int)]
        public UserRole Role { get; set; }
    }
}