using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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

            await categoryRepository.CreateAsync(category);

            //Domain model to DTO
            CategoryDTO response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandler = category.UrlHandler
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            //Map Domain Model to DTO
            var response = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandler = category.UrlHandler
                });
            }

            return Ok(response);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existCategory = await categoryRepository.GetById(id);

            if (existCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDTO
            {
                Id = existCategory.Id,
                Name = existCategory.Name,
                UrlHandler = existCategory.UrlHandler
            };

            return Ok(response);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDTO request)
        {
            // Convert DTO to Domain Model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandler = request.UrlHandler
            };

            category = await categoryRepository.UpdatedAsync(category);

            if (category is null)
            {
                return NotFound();
            }
            // Convert Domain Model to DTO
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandler = category.UrlHandler
            };
            return Ok(response);
        }
    }
}
