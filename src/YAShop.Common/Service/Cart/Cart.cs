﻿using Newtonsoft.Json;
using YAShop.Common.Domain;
using YAShop.Common.Domain.Checkout;

namespace YAShop.Common.Service.Cart;

public class Cart
{
    public Cart()
    {
    }
    
    public List<CartItem> Items { get; set; } = new();

    public CheckoutDetails? CheckoutDetails { get; set; }

    [JsonIgnore]
    public bool IsEmpty => TotalCount == 0;
    [JsonIgnore] 
    public int TotalCount => Items.Sum(i => i.QTY);
    [JsonIgnore] public decimal TotalAmount => Items.Sum(i => i.Price * i.QTY);

    public string ShippingStr => CheckoutDetails == null ? "Calculating" : Registry.Current.Cities.Find(CheckoutDetails.Address.CityId).Price.ToString("$0.00");
    public object CityName => CheckoutDetails==null ? "": Registry.Current.Cities.Find(CheckoutDetails.Address.CityId).NameWithPrice;
}

public class CartItem
{
    public Guid ProductId { get; set; }
    public string Title{ get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public int QTY { get; set; }
}