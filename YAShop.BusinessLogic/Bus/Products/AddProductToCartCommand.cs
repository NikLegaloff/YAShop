namespace YAShop.BusinessLogic.Bus.Products
{
    public class CancelOldNotPaidOrdersCommand : ICommand
    {
        public int MaxDurationHours{ get; set; }
    }
}