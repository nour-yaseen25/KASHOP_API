using KASHOP.DAL.Data;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Model;
using KASHOP.DAL.Repository;
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
        private readonly IStringLocalizer<SharedResources> _Localizer;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository,IStringLocalizer<SharedResources>localizer) {
            _categoryRepository = categoryRepository;
            _Localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Get() {

            var categories=_categoryRepository.GetAll();
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
            _categoryRepository.Create(category);
            
            return Ok(new
            {
                message = _Localizer["Success"].Value
            });
        }
    }
}

