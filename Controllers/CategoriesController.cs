using Freaky_Fashion_Api.Data;
using Freaky_Fashion_Api.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freaky_Fashion_Api.Controllers;

//  /api/categories
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext dbContext;
    public CategoriesController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    //GET  /api/categories
    [HttpGet]
    public IEnumerable<CategoryDto> GetCategories()
    {
        var categories = dbContext.Categories
        .Include(c => c.Products)
        .Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Image = c.Image,
            Slug = c.Slug,
            Products = c.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Photo = p.Photo,
                Label = p.Label,
                SKU = p.SKU,
                Kategori = p.Kategori,
                Price = p.Price
            }).ToList()
        })
        .ToList();

        return categories;
    }


    // GET  /api/categories/{id}
    [HttpGet("{id}")]
    public ActionResult<CategoryDto> GetCategoryById(int id)
    {
        var category = dbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Image = category.Image,
            Slug = category.Slug,
            Products = category.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Photo = p.Photo,
                Label = p.Label,
                SKU = p.SKU,
                Kategori = p.Kategori,
                Price = p.Price
            }).ToList()
        };

        return categoryDto;
    }

    // GET  /api/categories/search?slug=kläder
    [HttpGet("search")]
    public ActionResult<CategoryDto> GetCategoryBySlug(string slug)
    {
        var category = dbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Slug == slug);

        if (category == null)
        {
            return Ok(new List<ProductDto>());  // empty array (no products)
        }

        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Image = category.Image,
            Slug = category.Slug,
            Products = category.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Photo = p.Photo,
                Label = p.Label,
                SKU = p.SKU,
                Kategori = p.Kategori,
                Price = p.Price
            }).ToList()
        };

        return categoryDto;
    }
}
