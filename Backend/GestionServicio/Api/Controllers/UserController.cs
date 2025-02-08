using Api.Extensions;
using Api.Helpers;
using Application.Dtos.Request;
using Application.Interfaces;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidateRequestExtension))]
        [ServiceFilter(typeof(ValidateClaimExtension))]
        public async Task<IActionResult> GetListUser([FromBody] GenericRequest<FiltersRequest> request)
        {
            var userIdClaim = Util.GetUserIdClainToken(HttpContext);
            var response = await _userService.GetListUser(request, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidateRequestExtension))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.GetLoginToken(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("create")]
        [Authorize]
        [ServiceFilter(typeof(ValidateRequestExtension))]
        [ServiceFilter(typeof(ValidateClaimExtension))]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            var userIdClaim = Util.GetUserIdClainToken(HttpContext);
            request.UserAuthId = userIdClaim;

            var response = await _userService.CreateUser(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("approve/${idUser:int}")]
        [Authorize]
        [ServiceFilter(typeof(ValidateClaimExtension))]
        public async Task<IActionResult> ApproveUser(int idUser)
        {
            var userIdClaim = Util.GetUserIdClainToken(HttpContext);
            var response = await _userService.ApproveUser(idUser, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }
    }
}
