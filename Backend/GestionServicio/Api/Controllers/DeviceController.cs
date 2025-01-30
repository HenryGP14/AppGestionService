using Application.Dtos.Request;
using Application.Interfaces;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/service")]
    [ApiController]
    [Authorize]
    public class DeviceController(IDeviceService service) : ControllerBase
    {
        private readonly IDeviceService _service = service;

        [HttpPost("${serviceId:int}/device/create")]
        public async Task<IActionResult> CreateDeviceByService([FromBody]DeviceRequest request, int serviceId)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _service.CreateDevice(request, serviceId, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("${serviceId:int}/device/list")]
        public async Task<IActionResult> ListDeviceByService(int serviceId)
        {
            var response = await _service.GetDeviceByServiceId(serviceId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("${serviceId:int}/device/edit/${deviceId:int}")]
        public async Task<IActionResult> ListDeviceByService([FromBody] DeviceRequest request, int serviceId, int deviceId)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var userIdClaim = GetUserIdClainToken();
            if (userIdClaim == 0)
                return Unauthorized("Claim de usuario no encontrado.");
            var response = await _service.UpdateDevice(request, serviceId, userIdClaim, deviceId);
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
