using System;

namespace YAShop.BusinessLogic.Bus.Products
{
    public class AddProductToCartCommandHandler : AbstractCommandHandler<AddProductToCartCommand>
    {
        public AddProductToCartCommandHandler(CommandBus bus) : base(bus) {}

        public override void Process(AddProductToCartCommand command)
        {
            Registry.Current.Services.Cart.Add(command.SKU, command.Title, command.QTY);
        }
    }
}