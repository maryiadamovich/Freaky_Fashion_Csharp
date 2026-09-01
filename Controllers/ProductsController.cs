using Freaky_Fashion_Api.Contracts.Products;
using Freaky_Fashion_Api.Data;
using Freaky_Fashion_Api.Domain;
using Freaky_Fashion_Api.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Freaky_Fashion_Api.Controllers;


//api/products
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    /*private IList<Product> products = new List<Product>
    {
        new Product { Id = 1, 
                      Description = "description",
                      Kategori = "kategori", 
                      Label = "label", 
                      Name = "name", 
                      Photo = "photo",
                      Price = 120,
                      SKU = "sku" },
        new Product { Id = 2,
                      Description = "description2",
                      Kategori = "kategori2",
                      Label = "label2",
                      Name = "name2",
                      Photo = "photo2",
                      Price = 122,
                      SKU = "sku2" },
        new Product { Id = 3,
                      Description = "description2",
                      Kategori = "kategori2",
                      Label = "label2",
                      Name = "name",
                      Photo = "photo2",
                      Price = 122,
                      SKU = "sku2" }
    };*/

    private readonly AppDbContext dbContext;

    public ProductsController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    //GET  /api/products
    //GET  /api/products?name={name}
    [HttpGet]
    public IEnumerable<ProductDto> GetProducts([FromQuery] string? name)
    {
        var query = dbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        var productDtos = query.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Photo = product.Photo,
            Label = product.Label,
            SKU = product.SKU,
            Price = product.Price,
            Kategori = product.Kategori
        }).ToList();

        return productDtos;
    }

    //GET  /api/products/{id}   //gets one unique product
    [HttpGet("{id}")]

    public ActionResult<ProductDto> GetProduct(int id)
    {
        var product = dbContext.Products.FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Photo = product.Photo,
            Label = product.Label,
            SKU = product.SKU,
            Price = product.Price,
            Kategori = product.Kategori
        };

        return Ok(productDto);
    }

    //POST  /api/products
    [HttpPost]
    public ActionResult<ProductResponse> Create([FromBody] CreateProductResponse dto)
    {
        if (dto == null)
        {
            return BadRequest("Invalid input: request body cannot be null.");
        }

        if (string.IsNullOrEmpty(dto.Name))  //required fild
        {
            return BadRequest("Product name is required.");
        }

        if (dto.Price <= 0)  //required fild
        {
            return BadRequest("Price must be greater than zero.");
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Photo = dto.Photo,
            Label = dto.Label,
            SKU = dto.SKU,
            Price = dto.Price,
            Kategori = dto.Kategori,
        };

        try
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while saving the product: {ex.Message}");
        }

        var response = new ProductResponse(product.Id, product.Name, product.Description, product.Photo, product.Label, product.SKU, product.Price, product.Kategori);

        // 201 Created
        return Created("", response);
    }

    //DELETE  /api/produkts/4
    [HttpDelete("{id}")]

    public IActionResult Delete(int id)
    {
        var product = dbContext.Products.Find(id);

        if (product == null)
        {
            //Returnera 404 Not Found
            return NotFound();
        }

        dbContext.Products.Remove(product);

        dbContext.SaveChanges();

        //Returnera 204 No Content
        return NoContent();
    }
}
