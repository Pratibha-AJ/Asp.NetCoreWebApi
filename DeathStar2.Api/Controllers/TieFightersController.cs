using DeathStar2.Data.Contracts;
using DeathStar2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeathStar2.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TieFightersController : ControllerBase
    {
        private readonly ITieFighterRepository _tieFighterRepository;

        public TieFightersController(ITieFighterRepository tieFighterRepository)
        {
            _tieFighterRepository = tieFighterRepository;
        }

        [HttpGet]
        [Route("damaged")]
        public IActionResult List()
        {
            var results = _tieFighterRepository.GetDamagedTieFighters();
            return Ok(results);
        }

        [HttpGet]
        [Route("find/{code}")]
        public IActionResult Get(string code)
        {
            var item = _tieFighterRepository.GetTieFighterByCode(code);
            return Ok(item);
        }

        [HttpPatch]
        [Route("repair")]
        public IActionResult Repair()
        {
            var service = new TieFighterRepairService(_tieFighterRepository);
            service.RepairTieFighters();

            return Ok();
        }

        [HttpPatch]
        [Route("repair/{code}")]
        public IActionResult Repair(string code)
        {
            var service = new TieFighterRepairService(_tieFighterRepository);
            service.RepairTieFighterByCode(code);

            return Ok();
        }
    }
}