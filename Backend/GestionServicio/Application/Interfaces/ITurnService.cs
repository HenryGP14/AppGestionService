using Application.Dtos.Response;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;

namespace Application.Interfaces
{
    public interface ITurnService
    {
        Task<GenericResponse<DataResponse<TurnGestion>>> GetListTurnsAll(FiltersRequest request);
        Task<GenericResponse<bool>> CreateTurn(TurnAttention request);
    }
}
