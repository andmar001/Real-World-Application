using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDTO request)
        {
            //Map DTO to Domain Model
            Category category = new Category
            {
                Name = request.Name,
                UrlHandler = request.UrlHandler
            };

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            //Domain model to DTO
            CategoryDTO response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandler = category.UrlHandler
            };
            return Ok(response);
        }
    }
}
