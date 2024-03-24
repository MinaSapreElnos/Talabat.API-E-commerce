using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.API.Extentions;
using Talabat.Core.Models.Identity;
using Talabat.Core.Models.OrderAggragation;
using Talabat.Core.Services.Contract;
using Talabat.Core.Models.Identity;



namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthServise authServise;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<AppUser> userManager , 
            SignInManager<AppUser> signInManager,
            IAuthServise authServise,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.authServise = authServise;
            _mapper = mapper;
        }

        
        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
         {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);

            if (User == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(User, loginDto.Password,false);


            if (result.Succeeded is false)
            
                return Unauthorized();

            return Ok(new UserDto()
            {
                DisplayName = User.UserName,
                Email = User.Email,
                Token = await authServise.CreateTokenAcync(User, _userManager)
            });
            
        }




        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNamder,
                UserName = registerDto.Email.Split("@")[0],

            };

            var result = await _userManager.CreateAsync(User, registerDto.Password);

            if (result.Succeeded is false)
            {
                return BadRequest();
            }

            return Ok(new UserDto() 
            {
                DisplayName = registerDto.DisplayName,

                Email = registerDto.Email,

                Token = await authServise.CreateTokenAcync(User, _userManager) 

            });



        }




        [Authorize(AuthenticationSchemes = "Bearer")]
       
        [HttpGet("CurrentUser")]


        public async Task<ActionResult<UserDto>> CurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(Email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await authServise.CreateTokenAcync(user, _userManager)
            });
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetAddress")]
     

		public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            //var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindUserWithAdressAsync(User);

            var MappingAddress = _mapper.Map<Core.Models.Identity.Address, AddressDTO>(user.Address);

            return Ok(MappingAddress);

        }




        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDTO>> UpdeteUserAddress(AddressDTO UpdateAddress)
        {
            if (UpdateAddress == null)
            {
                return BadRequest();
            }

            var Address = _mapper.Map<AddressDTO, Talabat.Core.Models.Identity.Address>(UpdateAddress);

            //var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindUserWithAdressAsync(User);

            user.Address = Address;

            var Result = await _userManager.UpdateAsync(user);

            if (!Result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(UpdateAddress);

        }


    }


}
