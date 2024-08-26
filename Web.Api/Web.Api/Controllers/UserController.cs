using Microsoft.AspNetCore.Mvc;
using Web.BLL;
using Web.Model;

namespace Web.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SetUser([FromBody] Users user)
        {
            var result = await _userService.SetUserAsync(user);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            return StatusCode(StatusCodes.Status201Created, users);
            
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var rs = await _userService.GetByIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, rs);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userid)
        {
            var rs = await _userService.DaleteUserAsync(userid);
            return StatusCode(StatusCodes.Status200OK, rs);
        }
    }
}
