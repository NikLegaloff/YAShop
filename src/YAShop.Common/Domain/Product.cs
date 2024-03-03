namespace YAShop.Common.Domain;

public class Product : DomainObject
{
    public string SKU { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int QTY{ get; set; }
    public string? Image{ get; set; }
    public string? Description{ get; set; }
}