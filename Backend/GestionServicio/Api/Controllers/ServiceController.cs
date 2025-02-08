using Api.Extensions;
using Api.Helpers;
using Application.Dtos.Request;
using Application.Interfaces;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/service")]
    [ApiController]
    [Authorize]
    public class ServiceController(IServiceServices service) : ControllerBase
    {
        private readonly IServiceServices _service = service;

        [HttpPost]
        [ServiceFilter(typeof(ValidateRequestExtension))]
        public async Task<IActionResult> ListAllServices([FromBody] FiltersRequest request)
        {
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
        [ServiceFilter(typeof(ValidateRequestExtension))]
        [ServiceFilter(typeof(ValidateClaimExtension))]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest request)
        {
            var userIdClaim = Util.GetUserIdClainToken(HttpContext);
            var response = await _service.CreateService(request, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update/${idService:int}")]
        [ServiceFilter(typeof(ValidateRequestExtension))]
        [ServiceFilter(typeof(ValidateClaimExtension))]
        public async Task<IActionResult> UpdateService([FromBody] ServiceRequest request, int idService)
        {
            var userIdClaim = Util.GetUserIdClainToken(HttpContext);
            var response = await _service.UpdateService(request, idService, userIdClaim);
            return StatusCode(response.StatusCode, response);
        }
    }
}
