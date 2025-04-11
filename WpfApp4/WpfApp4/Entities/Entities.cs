using System;

namespace WpfApp4.Entities
{
    public class Entities
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
    }

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }

        public int? ManufacturerId { get; set; }
        public Entities Manufacturer { get; set; }

        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public string Unit { get; set; }
        public decimal Price { get; set; }

        public Discount Discount { get; set; }
    }

    public class Discount
    {
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? MaxAllowedDiscount { get; set; }
    }
}
