using Microsoft.AspNetCore.Mvc;
using MyApp.Services;
using MyApp.Models;
using System.Threading.Tasks;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var userProfile = await _userProfileService.GetUserProfileAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userProfileService.CreateUserProfileAsync(userProfile);
            return CreatedAtAction(nameof(GetUserProfile), new { id = userProfile.Id }, userProfile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] UserProfile userProfile)
        {
            if (id != userProfile.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _userProfileService.UpdateUserProfileAsync(userProfile);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            var deleted = await _userProfileService.DeleteUserProfileAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}