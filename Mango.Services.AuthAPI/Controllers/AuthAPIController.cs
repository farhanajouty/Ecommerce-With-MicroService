using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto { get; set; }
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!String.IsNullOrEmpty(errorMessage))
            {
               _responseDto.Message= errorMessage;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
 
            }
            else
                return Ok(_responseDto);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User == null)
            {

                _responseDto.IsSuccess= false;
                _responseDto.Message = "User Name or Password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginResponse;

            return Ok(_responseDto);
        }
    }
}
