using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DeathStar2.Model;
using DeathStar2.Services.Contracts;
using DeathStar2.Api.CustomValidationHelper;

namespace DeathStar2.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SuperLaserController : ControllerBase
    {
        private const decimal MaxCharge= 100.0M;

        private readonly ISuperLaserService _superLaserService;

        public SuperLaserController(ISuperLaserService superLaserService)
        {
            _superLaserService = superLaserService;
            //_superLaserService.CreateSuperLaserTable();
        }

        [HttpGet]
        [Route("GetPower")]
        public IActionResult Get()
        {
            var results = _superLaserService.GetCapacity();
            return Ok(results);
        }

        [HttpPatch]        
        [Route("Charge")]
        public IActionResult ChargePower()
        {
            if(_superLaserService.CheckMaxCharge())
            _superLaserService.Charge();
            else
                return Ok(" Exceeds max charge");

            return Ok();
        }

        [HttpPatch]
        [Route("SetPower")]
        public IActionResult SetPower(decimal capacity)
        {
            _superLaserService.SetCapacity(capacity);
            return Ok();
        }

        [HttpPatch]        
        [Route("FirePower")]
        public IActionResult FirePower(Target target)
        {
           var result = _superLaserService.Fire(target);
            return Ok(result);
        }


    }

}