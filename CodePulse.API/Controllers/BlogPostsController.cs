using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDTO request)
        {
            //Convert DTO to Domain Model
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandler = request.UrlHandler
            };

            await blogPostRepository.CreateAsync(blogPost);

            //Convert Domain Model to DTO
            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandler = request.UrlHandler
            };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            
            //Convert Domain Model to DTO
            var response = new List<BlogPostDTO>();
            foreach (var blog in blogPosts)
            {
                response.Add(new BlogPostDTO
                {
                    Id = blog.Id,
                    Author = blog.Author,
                    Content = blog.Content,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    IsVisible = blog.IsVisible,
                    PublishedDate = blog.PublishedDate,
                    ShortDescription = blog.ShortDescription,
                    Title = blog.Title,
                    UrlHandler = blog.UrlHandler
                });
            }

            return Ok(response);
        }
    }
}
