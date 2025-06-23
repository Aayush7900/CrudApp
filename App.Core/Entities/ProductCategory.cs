namespace App.Core.Entities {
    public class ProductCategory {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
