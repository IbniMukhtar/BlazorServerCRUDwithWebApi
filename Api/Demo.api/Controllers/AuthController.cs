using Demo.api.Dmain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid registration data" });
            }

            var existingUser = await _userManager.FindByNameAsync(model.Username!);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Username is already taken" });
            }

            var user = new IdentityUser
            {
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password!);
            if (result.Succeeded)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password!, false, false);
                if (signInResult.Succeeded)
                {
                    return Ok(new { Message = "Registration and login successful", UserId = user.Id });
                }
                return BadRequest(new { Message = "Failed to sign in after registration" });
            }

            // If registration fails, return detailed error messages
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Message = "Registration failed", Errors = errors });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegisteredUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, false, false);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Login successful" });
            }
            return BadRequest(new { Message = "Invalid username or password" });
        }

    }
}
