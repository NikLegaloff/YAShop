﻿using YAShop.Common.Domain;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Models
{
    public class ProductSummary
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int QTY { get; set; }
        public string? Image { get; set; }
    }

    public class IndexModel
    {
        public ProductSummary[] TopProducts { get; set; }
        public ProductSummary[] LatestProducts { get; set; }
    }

}
    public class ProductViewModel
    {
        public Product Product{ get; set; }
        public ProductSummary[] SimilarProducts { get; set; }
        
    }

