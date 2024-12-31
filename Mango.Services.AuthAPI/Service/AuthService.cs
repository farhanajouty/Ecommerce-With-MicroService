using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _db = db;
            _userManager = userManager;
           _roleManager = roleManager;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(t => t.UserName.ToLower() ==loginRequest.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user,loginRequest.Password);

            if (user != null || isValid == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
             }

            UserDto userDto = new()
            {
                Name = user.UserName,
                ID = user.Id,
                Email=user.Email,
                PhoneNumber = user.PhoneNumber

            };


            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {

                User = userDto,
                Token = ""
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber


            };
            try
            {
                var result =await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {

                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                    if (userToReturn != null)
                    {
                        UserDto userdto = new()
                        {
                            Email = userToReturn.Email,
                            Name = registrationRequestDto.Name,
                            ID = userToReturn.Id,
                            PhoneNumber= userToReturn.PhoneNumber

                        };
                        return "";

                    }
                }
                else
                {
                    return "error Occured";
                }
            }
            catch (Exception ex)
            {

                
            }
            return "error Occured";
        }
    }
}
