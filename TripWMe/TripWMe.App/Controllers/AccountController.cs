using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripWMe.CoreHelpers.Attributes;
using TripWMe.CoreHelpers.ErrorHeandling;
using TripWMe.Domain.User;
using TripWMe.Models.User;

namespace TripWMe.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signinManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AccountController(UserManager<TUser> userManager, SignInManager<TUser> signinManager
            , ILogger<AccountController> logger, IConfiguration config, IMapper mapper)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _logger = logger;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.tripwme.createnewusertoken+json" })]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if(user != null)
                {
                    var result = await _signinManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if(result.Succeeded)
                    {
                        //Create new Token
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _config["Tokens:Issuer"],
                            _config["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: credentials
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
                

            }
            return BadRequest();
        }

        [HttpPost]
        [RequestHeaderMatchesMediaType("Accept", new[] { "application/vnd.tripwme.createnewuser+json" })]
        public async Task<IActionResult> Post([FromBody]TUserForCreationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<TUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            return new OkObjectResult("Account created");
        }
    }
}
