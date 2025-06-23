using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities {
    public class Product {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Price { get; set; }
        public string? ImagePath { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }

    }
}
