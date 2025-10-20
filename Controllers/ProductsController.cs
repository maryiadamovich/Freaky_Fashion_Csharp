using Freaky_Fashion_Api.Data;
using Freaky_Fashion_Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Freaky_Fashion_Api.Contracts.Products;

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
    [HttpGet]

    public IEnumerable<Product> GetProducts()
    {
        var products = dbContext.Products.ToList();

        return products;
    }

    //GET  /api/products/{id}   //gets one unique product
    [HttpGet("{id}")]

    public ActionResult<Product> GetProduct(int id)
    {
        var products = dbContext.Products;
        var product = products.FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    //GET  /api/products/search?name={name}   //get a list of products with the same name
    [HttpGet("search")]
    public ActionResult<List<Product>> GetProductByName([FromQuery] string name)
    {
        var products = dbContext.Products;
        var productsByName = products.Where(x => x.Name == name).ToList();

        if (productsByName.Count == 0)
        {
            return new List<Product>();
        }

        return productsByName;
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
