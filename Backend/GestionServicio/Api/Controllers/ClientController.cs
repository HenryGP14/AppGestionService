using Application.Dtos.Request;
using Application.Interfaces;
using Azure.Core;
using Infraestructure.Commons.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    [Authorize]
    public class ClientController(IClientService service,  IContractService contractService) : ControllerBase
    {
        private readonly IClientService _service = service;
        private readonly IContractService _serviceContract = contractService;

        [HttpPost]
        public async Task<IActionResult> ListAllUserClient([FromBody] FiltersRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _service.GetListClients(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("${idClient:int}")]
        public async Task<IActionResult> GetUserClientId(int idClient)
        {
            var response = await _service.GetCLientById(idClient);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserClient([FromBody] ClientRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _service.CreateUserClient(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("editar/${idClient:int}")]
        public async Task<IActionResult> UpdateUserClient([FromBody] ClientRequest request, int idClient)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _service.UpdateUserClient(request, idClient);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("createContract")]
        public async Task<IActionResult> CreateContracctClient([FromBody] ContractRequest request)
        {
            if (request is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "El request no existe datos");
            }
            var response = await _serviceContract.CreateContractClient(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("${idClient:int}/view/contract")]
        public async Task<IActionResult> ViewContract(int idClient)
        {
            var response = await _serviceContract.GetContractByClientId(idClient);
            return StatusCode(response.StatusCode, response);
        }
    }
}
