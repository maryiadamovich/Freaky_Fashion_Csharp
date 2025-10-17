namespace Freaky_Fashion_Api.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; } 

        public string? Photo { get; set; }

        public string? Label { get; set; }

        public string? SKU { get; set; }

        public int Price { get; set; }

        public int? Kategori { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
