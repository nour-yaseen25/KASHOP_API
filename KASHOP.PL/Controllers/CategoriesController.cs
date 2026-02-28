using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.PL.Resources;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace KASHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResources> _Localizer;

        public CategoriesController(ApplicationDbContext context,IStringLocalizer<SharedResources>localizer) { 
            _context = context;
            _Localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Get() {

            var categories = _context.Categories.Include(c => c.Translations).ToList();
            var response=categories.Adapt<List<CategoryResponse>>();    

            return Ok(new
            {
                data=response,
                _Localizer["Success"].Value
            }); 
        }
        [HttpPost("")]

        public IActionResult Create(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _context.Add(category);
            _context.SaveChanges();
            return Ok(new
            {
                message = _Localizer["Success"].Value
            });
        }
    }
}

