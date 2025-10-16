using Freaky_Fashion_Api.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Freaky_Fashion_Api.Controllers;

//  /api/categories
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    // GET  /api/categories/{id}
    [HttpGet("{id}")]
    public ActionResult<CategoryDto> GetCategoryById(int id)
    {
        var categoryDto = new CategoryDto();

        return categoryDto;
    }
}
