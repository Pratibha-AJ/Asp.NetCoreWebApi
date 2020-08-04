using DeathStar2.Data.Contracts;
using DeathStar2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeathStar2.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class HatchesController : ControllerBase
    {
        private readonly IHatchRepository _hatchRepository;

        public HatchesController(IHatchRepository hatchRepository)
        {
            _hatchRepository = hatchRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            var results = _hatchRepository.GetHatches();
            return Ok(results);
        }

        [HttpPatch]
        [Route("{id}/open")]
        public IActionResult Open(int id)
        {
            var result = new HatchService(_hatchRepository).OpenHatch(id);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}/close")]
        public IActionResult Close(int id)
        {
            var result = new HatchService(_hatchRepository).CloseHatch(id);
            return Ok(result);
        }
    }
}