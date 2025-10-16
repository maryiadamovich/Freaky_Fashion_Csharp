namespace Freaky_Fashion_Api.Dtos.Categories;

public record CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Slug { get; set; }
    public IReadOnlyList<ProductDto> Products { get; set; } = new List<ProductDto>();

}
