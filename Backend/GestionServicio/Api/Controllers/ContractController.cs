using Application.Dtos.Request;
using Application.Interfaces;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/contract")]
    [ApiController]
    [Authorize]
    public class ContractController(IContractService service) : ControllerBase
    {
        private readonly IContractService _service = service;

        [HttpPut("upgrade/service")]
        public async Task<IActionResult> UpgradeContractService([FromBody] UpgradeServiceRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _service.UpgradeServiceContract(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("change/status")]
        public async Task<IActionResult> ChangeContract([FromBody] UpgradeServiceRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _service.UpdateStateContract(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
