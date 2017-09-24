using System.Collections.Generic;

namespace YAShop.BusinessLogic.Presistense.MSSQL
{
    public class PageData<TRes>
    {
        public int TotalRows { get; set; }
        public IEnumerable<TRes> Rows { get; set; }
    }
}