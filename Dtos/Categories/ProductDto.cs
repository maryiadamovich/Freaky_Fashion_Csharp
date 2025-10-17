namespace Freaky_Fashion_Api.Dtos.Categories
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? Photo { get; set; }

        public string? Label { get; set; }

        public string? SKU { get; set; }

        public int Price { get; set; }

        public int? Kategori { get; set; }
   
    }
}
