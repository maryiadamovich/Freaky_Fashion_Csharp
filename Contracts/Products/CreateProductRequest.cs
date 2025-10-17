namespace Freaky_Fashion_Api.Contracts.Products;

public record CreateProductResponse
(
    string Name,
    string? Description,
    string? Photo,
    string? Label,
    string? SKU,
    int Price,
    int? Kategori
  );
