using System;
using System.Data;
using YAShop.BusinessLogic.Presistense;

namespace YAShop.BusinessLogic.DomainModel
{
    public class Category : DomainObject
    {
        [DBField(SqlDbType.NVarChar, 128)]
        public string Name{ get; set; }
        [DBField(SqlDbType.UniqueIdentifier,0,true)]
        public Guid? ParentId{ get; set; }
    }
}