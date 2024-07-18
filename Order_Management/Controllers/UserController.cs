using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order_Manag.Core.Entites.Identity;
using Order_Manag.Core.ServicesContract;
using Order_Management.DTOS;
using Order_Management.HandlingErrors;
using System.Security.Claims;

namespace Order_Management.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new User()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhonNumber,
                Role=model.Role,
                UserName = model.Email.Split('@')[0],
            };

            var Result = await _userManager.CreateAsync(user, model.Password);
            if (!Result.Succeeded) return BadRequest(new ApisResponse(400));

            var ReturnedUser = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Role = user.Role,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(ReturnedUser);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApisResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);


            if (Result.Succeeded is false)
                return Unauthorized(new ApisResponse(401));
            return Ok(new UserDto()
            {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Role= user.Role,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)


            });

        }

      


    }
}
