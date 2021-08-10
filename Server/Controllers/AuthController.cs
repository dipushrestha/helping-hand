using AutoMapper;
using helping_hand.Models;
using helping_hand.Models.Dto;
using helping_hand.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helping_hand.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IAuthManager _authManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly EmailSender _emailSender;

        public AuthController
        (
            UserManager<ApiUser> userManager,
            IAuthManager authManager,
            ILogger<AuthController> logger,
            IMapper mapper,
            EmailSender emailSender
        )
        {
            _userManager = userManager;
            _authManager = authManager;
            _logger = logger;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            _logger.LogInformation($"Registration attempt for {registerDto.UserName}");

            if (!ModelState.IsValid)
            {
                Console.Write("invalid");
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(registerDto);
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest("User registration attempt failed!");
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var param = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", user.Email }
                };

                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var verificationUrl = QueryHelpers.AddQueryString($"{baseUrl}/auth/confirm-email", param);

                await _emailSender.SendEmailConfirmation(verificationUrl, user.Email);
                await _userManager.AddToRolesAsync(user, registerDto.Roles);

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)} of the {nameof(AuthController)}.");
                return Problem($"Something went wrong in the {nameof(Register)}.", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation($"Login attempt for {loginDto.UserName}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(loginDto))
                {
                    return Unauthorized(new Error { Title = "Invalid Credentials", Status = 401 });
                }

                if (!await _authManager.ValidateEmail(loginDto))
                {
                    return Unauthorized(new Error { Title = "Email is not confirmed", Status = 401 });
                }

                return Accepted(new { Token = await _authManager.CreateToken() });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)} of the {nameof(AuthController)}.");
                return Problem($"Something went wrong in the {nameof(Login)}.", statusCode: 500);
            }
        }

        [HttpGet("EmailConfirmation")]
        [Route("confirm-email")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest(new Error { Title = "Invalid Email Confirmation Request", Status = 401 });

            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);

            if (!confirmResult.Succeeded)
                return BadRequest(new Error { Title = "Invalid Email Confirmation Request", Status = 401 });

            return Ok();
        }
    }
}
