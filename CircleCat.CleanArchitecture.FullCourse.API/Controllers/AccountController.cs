using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.User;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services;
using CircleCat.CleanArchitecture.FullCourse.Domain.Common;
using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CircleCat.CleanArchitecture.FullCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, IMapper mapper, IAuthenticationService authManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var user = _mapper.Map<User>(dto);
                    user.SecureRandomNumber = DateTime.Now.Ticks.ToString();
                    var result = await _userManager.CreateAsync(user, dto.Password);
                    var errors = new List<string>();
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            errors.Add(error.Code);
                        }
                        return Ok(new BaseResponse<User>()
                        {
                            Success = result.Succeeded,
                            Message = System.String.Join(";", errors)
                        });
                    }
                    await _userManager.AddToRolesAsync(user, dto.Roles);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    return Ok(new BaseResponse<object>()
                    {
                        Success = result.Succeeded,
                        Result = new
                        {
                            Token = token
                        }
                    });

                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] UserConfirmEmailDTO ce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(ce.Email);
                if (user == null)
                {
                    var msg = "Người dùng không tồn tại";
                    return Accepted(new BaseResponse<object>
                    {
                        Success = false,
                        Message = msg
                    });
                }
                var result = await _userManager.ConfirmEmailAsync(user, ce.Token);

                if (result.Succeeded)
                {
                    return Accepted(new BaseResponse<object>
                    {
                        Success = true,
                    });
                }
                else
                {
                    var msg = "Token không hợp lệ";
                    return Accepted(new BaseResponse<object>
                    {
                        Success = false,
                        Message = msg
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(dto))
                {
                    var error = "Thông tin đăng nhập không chính xác! Xin hãy thử lại.";
                    return Accepted(new BaseResponse<object>()
                    {
                        Success = false,
                        Message = error
                    });
                }
                var u = await _userManager.FindByEmailAsync(dto.Email);
                if (u == null)
                {
                    var msg = "Không tìm thấy người dùng";
                    return Accepted(new BaseResponse<object>()
                    {
                        Success = false,
                        Message = msg
                    });
                }
                if (!u.EmailConfirmed)
                {
                    var error = "Bạn chưa xác thục tài khoản! Hãy kiểm tra email để xác thực.";
                    return Accepted(new BaseResponse<object>()
                    {
                        Success = false,
                        Message = error
                    });
                }
                var roles = await _userManager.GetRolesAsync(u);
                return Accepted(new BaseResponse<object>()
                {
                    Success = true,
                    Result = new
                    {
                        Token = await _authManager.CreateToken(),
                        User = u,
                        Roles = roles
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
