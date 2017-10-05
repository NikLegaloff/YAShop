using System;
using System.Threading.Tasks;
using YAShop.BusinessLogic.DomainModel;

namespace YAShop.BusinessLogic.Bus.Products
{
    public class CancelOldNotPaidOrdersCommandHandler : AbstractCommandHandler<CancelOldNotPaidOrdersCommand>
    {
        public CancelOldNotPaidOrdersCommandHandler(Guid dtoId) : base(dtoId){}

        public override async Task<bool> Process(CancelOldNotPaidOrdersCommand command)
        {
            var orders = await Registry.Current.Data.Orders.Select(" where State=0 and Date<@date", new {date = DateTime.Now.AddHours(-command.MaxDurationHours)});
            if (orders != null && orders.Length>0)
            {
                await Status(orders.Length + " orders to cancel");
                foreach (var order in orders)
                {
                    await Registry.Current.Services.Order.Cancel(order);
                }
            }
            else await Status("No orders to cancel");
            
            return true;
        }
    }
}