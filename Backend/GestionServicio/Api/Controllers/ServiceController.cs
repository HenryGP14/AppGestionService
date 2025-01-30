using Application.Dtos.Request;
using Application.Interfaces;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/service")]
    [ApiController]
    [Authorize]
    
    public class ServiceController(IServiceServices service) : ControllerBase
    {
        private readonly IServiceServices _service = service;

        [HttpPost]
        public async Task<IActionResult> ListAllServices([FromBody] FiltersRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _service.GetListServices(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("${idService:int}")]
        public async Task<IActionResult> ListAllServices(int idService)
        {
            var response = await _service.GetServiceById(idService);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _service.CreateService(request, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update/${idService:int}")]
        public async Task<IActionResult> UpdateService([FromBody] ServiceRequest request, int idService)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _service.UpdateService(request, idService, userIdClaim);
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
