using final_api_project.DBcontext;
using final_api_project.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace final_api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(JwtOptions jwtOptions , UniversityContext dbcontext) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public ActionResult<string> AuthenticateUser(AuthenticationRequest authenticationRequest)
        {
            var user = dbcontext.Users.FirstOrDefault(x => x.Name == authenticationRequest.Username &&
            x.Password == authenticationRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var tokenhander = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
            //    Expires = DateTime.UtcNow.AddMinutes(jwtOptions.LifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()) ,
                    new (ClaimTypes.Name, user.Name) 
                })
            };
            var securedToken = tokenhander.CreateToken(tokenDescriptor);
            var accessToken = tokenhander.WriteToken(securedToken);
            return Ok(accessToken);
        }

    }
}
