namespace App.Application.DTO {
    public class ProductDTO {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? ImagePath { get; set; }
        public int ProductCategoryId { get; set; }
    };

}
