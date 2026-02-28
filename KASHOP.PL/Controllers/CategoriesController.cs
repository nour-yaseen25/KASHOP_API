using KASHOP.DAL.Data;
using KASHOP.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(_Localizer["Success"].Value); 
        }
    }
}
