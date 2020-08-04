using DeathStar2.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeathStar2.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TargetsController : ControllerBase
    {
        private IKnownTargetsRepository _knownTargetsRepository;

        public TargetsController(IKnownTargetsRepository knownTargetsRepository)
        {
            _knownTargetsRepository = knownTargetsRepository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult List()
        {
            var results = _knownTargetsRepository.GetKnownTargets();
            return Ok(results);
        }
    }
}