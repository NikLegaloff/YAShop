using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace YAShop.BusinessLogic.Presistense
{
    public class DomainObject
    {
        public DomainObject()
        {
            Id = Guid.Empty;
        }

        public int __RowsTotal { get; set; }

        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }

        public override string ToString()
        {
            return this.GetType().Name + " " + Id + "\n" +  string.Join(", ", this.GetType().GetProperties().Where(t=>t.Name!="Id" && t.GetValue(this)!=null).Select(t => t.Name + ": " + t.GetValue(this)).Where(s=>!s.Contains("List`1")));
        }
    }
}