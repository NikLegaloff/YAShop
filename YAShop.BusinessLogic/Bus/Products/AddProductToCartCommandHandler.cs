using System;
using YAShop.BusinessLogic.DomainModel;

namespace YAShop.BusinessLogic.Bus.Products
{
    public class AddProductToCartCommandHandler : AbstractCommandHandler<AddProductToCartCommand>
    {
        public AddProductToCartCommandHandler(CommandBus bus) : base(bus) {}

        public override void Process(AddProductToCartCommand command)
        {
            var cart = Registry.Current.Services.Cart.GetCart();
            cart.Items.Add(new CartItem(command.SKU, command.Title, command.QTY));
        }
    }
}