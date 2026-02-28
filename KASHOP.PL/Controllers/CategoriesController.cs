using KASHOP.BLL.Service;
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
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, IStringLocalizer<SharedResources>localizer) {
            _categoryService = categoryService;
            _Localizer = localizer;
        }

        [HttpGet("")]
        public IActionResult Index() {

            var categories= _categoryService.GetAllCategories();
            /*  var categories=_categoryRepository.GetAll();
            var response=categories.Adapt<List<CategoryResponse>>();  */
            return Ok(new
            {
                data=categories,
                _Localizer["Success"].Value
            }); 
        }
        [HttpPost("")]

        public IActionResult Create(CategoryRequest request)
        {
            var response = _categoryService.CreateCategory(request);
 /*
            var category = request.Adapt<Category>();
            _categoryRepository.Create(category);   */
            return Ok(new
            {
                message = _Localizer["Success"].Value,
                response
            });
        } 
    }
}

