using Application.Dtos.Request;
using Application.Interfaces;
using Azure.Core;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetListUser([FromBody] FiltersRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _userService.GetListUser(request, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _userService.GetLoginToken(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");

            request.UserAuthId = userIdClaim;

            var response = await _userService.CreateUser(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("approve/${idUser:int}")]
        [Authorize]
        public async Task<IActionResult> ApproveUser(int idUser)
        {
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _userService.ApproveUser(idUser, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }

        private int GetUserIdClainToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
                return 0;
            // Extraer los claims del token
            var userClaims = identity.Claims;
            // Obtener el ID del usuario desde el claim 'unique_name'
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (userIdClaim == null)
                return 0;

            return int.Parse(userIdClaim.Value);
        }
    }
}
