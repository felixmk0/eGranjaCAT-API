using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nastrafarmapi.DTOs.Users;
using nastrafarmapi.Extensions;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync(CreateUserDTO userDTO)
        {
            var result = await userService.CreateUserAsync(userDTO);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(LoginUserDTO userDTO)
        {
            var result = await userService.LoginUserAsync(userDTO);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await userService.GetUsersAsync();
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            int loggedUserId = User.GetUserId();
            bool isAdmin = User.IsInRole("Admin");

            if (!isAdmin && id != loggedUserId) return Forbid("No tens permís per veure aquesta informació");

            var result = await userService.GetUserByIdAsync(id);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var result = await userService.DeleteUserById(id);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(new { Message = "User deleted successfully" });
        }
    }
}
