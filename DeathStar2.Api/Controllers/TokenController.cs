using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DeathStar2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetToken()
        {
            var sharedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("security-using-token"));
            var creds = new SigningCredentials(sharedKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityTokenHandler().CreateEncodedJwt(new SecurityTokenDescriptor()
            {
                Audience = "",
                IssuedAt = DateTime.Now,
                Issuer = "",
                Expires = DateTime.Now.AddDays(1),
                NotBefore = DateTime.Now,
                SigningCredentials = creds
            });

            return Ok(token);
        }
    }
}