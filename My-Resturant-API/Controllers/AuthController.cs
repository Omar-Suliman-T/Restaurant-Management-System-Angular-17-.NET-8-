using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Auth;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Interfaces;
using Serilog;

namespace My_Resturant.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authantication;
        private readonly IPersonServices _personServices;
        private readonly IEmailService _emailService;
        private readonly TokenHelper tokenHelper;
        private readonly RestDbContext _context;
        public AuthController(IAuthServices authantication, IPersonServices personServices, RestDbContext context, TokenHelper tokenHelper)
        {
            _authantication = authantication;
            _personServices = personServices;
            _context = context;
            this.tokenHelper = tokenHelper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LogIn([FromBody] LogInDTOs input)
        {
            try
            {
                var response = await _authantication.LogIn(input);
                return response.Equals("Authantication Failed") ? Unauthorized("Email Or Password Is Not Correct") : Ok(response);

            }
            catch (Exception ex)
            {

                return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });

            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LogOut([FromHeader] string token)
        {
            try
            {

                await _authantication.LogOut(token);

                return Ok("You logged out successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetRequest([FromBody] ResetRequestDTOs dto)
        {
            try
            {
                var user = await _personServices.GetPersonByEmailAsync(dto.email);
                if (user == null) return Ok();


                var code = new Random().Next(100000, 999999).ToString();


                user.passwordResetCode = code;
                user.passwordResetExpiry = DateTime.UtcNow.AddMinutes(10);
                await _context.SaveChangesAsync();


                await _emailService.SendEmailAsync(dto.email, "Password Reset Code",
                    $"Your password reset code is <b>{code}</b>. It expires in 10 minutes.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> VerifyReset([FromBody] ResetRequestDTOs dto)
        {
            try
            {


                var user = await _personServices.GetPersonByEmailAsync(dto.email);
                if (user == null) return BadRequest("Invalid request.");


                if (user.passwordResetCode != dto.code || user.passwordResetExpiry < DateTime.UtcNow)
                    return BadRequest("Invalid or expired code.");


                var token = await tokenHelper.GeneratePasswordResetTokenAsync(user.id.ToString());
                var personId = await tokenHelper.ValidatePsswordResetTokenAsync(token);

                user.password = dto.newPassword;

                user.passwordResetCode = null;
                user.passwordResetExpiry = null;
                await _context.SaveChangesAsync();

                return Ok("Password has been reset.");
            }

            catch(Exception ex)
            {
                return StatusCode(500, new { massage = ex.Message, ex.InnerException?.Message });

            }
        }   
       
    }
}
