using Application.Dtos.Request;
using Application.Interfaces;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/cash")]
    [ApiController]
    [Authorize]
    public class CashController(ICashService service) : ControllerBase
    {
        private readonly ICashService _service = service;

        [HttpPost]
        public async Task<IActionResult> GetCashList([FromBody] FiltersRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _service.GetListCashesAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCash([FromBody] CashRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            request.UserId = userIdClaim;

            var response = await _service.CreateCashAsync(request);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost("${castId:int}")]
        public async Task<IActionResult> GetCashById(int castId)
        {
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _service.GetCashByIdAsync(castId, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("edit/${cashId:int}")]
        public async Task<IActionResult> UpdateCash([FromBody] CashRequest request, int cashId)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            request.UserId = userIdClaim;

            var response = await _service.UpdateCashAsync(request, cashId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("asignate")]
        public async Task<IActionResult> AsignateUserCash(CashAsignateRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            request.UserIdAuth = userIdClaim;
            var response = await _service.AsignateUserCash(request);
            return StatusCode(response.StatusCode, response);
        }

        private int GetUserIdClainToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return 0;
            }
            // Extraer los claims del token
            var userClaims = identity.Claims;
            // Obtener el ID del usuario desde el claim 'unique_name'
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (userIdClaim == null)
            {
                return 0;
            }

            return int.Parse(userIdClaim.Value);
        }
    }
}
