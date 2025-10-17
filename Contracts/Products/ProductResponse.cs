namespace Freaky_Fashion_Api.Contracts.Products;

public record ProductResponse(
    int Id,
    string Name,
    string? Description,
    string? Photo,
    string? Label,
    string? SKU,
    int Price,
    int? Kategori
);
