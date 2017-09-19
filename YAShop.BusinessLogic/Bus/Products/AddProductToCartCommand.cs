namespace YAShop.BusinessLogic.Bus.Products
{
    public class AddProductToCartCommand : ICommand
    {
        public string SKU { get; set; }
        public string Title{ get; set; }
        public int QTY{ get; set; }
    }
}