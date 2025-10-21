using Freaky_Fashion_Api.Data;
using Freaky_Fashion_Api.Domain;
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

    // POST  /api/categories
    [HttpPost]
    public ActionResult<CategoryDto> AddCategory([FromBody] CategoryDto createCategoryDto)
    {
        if (createCategoryDto == null || string.IsNullOrEmpty(createCategoryDto.Name))
        {
            return BadRequest("Category data is invalid.");
        }

        var slug = generateSlug(createCategoryDto.Name); // generate the slug

        var image = $"https://placehold.co/300x400/grey/white?text={Uri.EscapeDataString(createCategoryDto.Name)}";  //  generate the img

        var category = new Category
        {
            Name = createCategoryDto.Name,
            Slug = slug,
            Image = image,
            Products = new List<Product>()  
        };

        dbContext.Categories.Add(category);

        dbContext.SaveChanges();

        var categoryDto = new CategoryDto  //  create response
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Image = category.Image,
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

        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDto);
    }

    private string generateSlug(string name)
    {
        var slug = name.ToLower()
                       .Replace(" ", "-")
                       .Where(c => char.IsLetterOrDigit(c) || c == '-')
                       .Aggregate("", (current, c) => current + c);

        return slug;
    }

    // DELETE  /api/categories/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(int id)
    {
        var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound($"Category with ID {id} was not found.");
        }

        dbContext.Categories.Remove(category);
        dbContext.SaveChanges();

        return NoContent(); // 204 - standardrespons 
    }


}
