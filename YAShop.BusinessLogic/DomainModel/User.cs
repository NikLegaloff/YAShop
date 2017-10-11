using System;
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
        public static Guid SuperuserId =  new Guid("11111b33-77f9-4754-8346-0960d8d84f8f");

        [DBField(SqlDbType.NVarChar, 128)]
        public string Email { get; set; }
        [DBField(SqlDbType.NVarChar, 128)]
        public string Password{ get; set; }
        [DBField(SqlDbType.Int)]
        public UserRole Role { get; set; }
    }
}