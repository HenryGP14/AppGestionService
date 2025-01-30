using Application.Dtos.Response;
using Application.Interfaces;
using Application.Validations;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Utility.Static;

namespace Application.Services
{
    internal class TurnService : ITurnService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TurnValidator _validations;

        public TurnService(IUnitOfWork unitOfWork, TurnValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _validations = validationRules;
        }

        public async Task<GenericResponse<bool>> CreateTurn(TurnAttention request)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var validationResult = await _validations.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    response.Erros = validationResult.Errors;
                    return ErrorResponse(response, "Error por validación", StatusCodes.Status400BadRequest);
                }

                var cashExists = await _unitOfWork.Cash.GetCashByIdAsync(request.CashId);
                if (cashExists is null)
                {
                    return ErrorResponse(response, "Cajero no encontrado", StatusCodes.Status404NotFound);
                }

                var userExists = await ValidateUserAsync(request.UsarAuthId);
                if (userExists is null || !userExists!.RolRolid.Equals((int)UserRole.Cajero) || userExists.UserstatusStatusid.Equals((int)UserState.Inactivo))
                {
                    return ErrorResponse(response, "Este usuario no tiene permitido realizar esta acción", StatusCodes.Status401Unauthorized);
                }
                var result = await _unitOfWork.Turn.CreateTurnAttention(request);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "CreateTurn");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<DataResponse<TurnGestion>>> GetListTurnsAll(FiltersRequest request)
        {
            var response = new GenericResponse<DataResponse<TurnGestion>>();
            try
            {
                var listTruns = await _unitOfWork.Turn.GetListTurnsAsync(request);
                if (listTruns is null)
                {
                    return ErrorResponse(response, "Registro no encontrados", StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, listTruns);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetListTurnsAll");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<User?> ValidateUserAsync(int userId)
        {
            if (userId == 0)
                return null;

            var userExists = await _unitOfWork.User.GetUserByIdAsync(userId);
            if (userExists == null)
                return null;

            return userExists;
        }

        private GenericResponse<T> ErrorResponse<T>(GenericResponse<T> response, string message, int statusCode)
        {
            response.Success = false;
            response.Message = message;
            response.StatusCode = statusCode;
            return response;
        }

        private GenericResponse<T> SuccessResponse<T>(GenericResponse<T> response, T data, string message = "")
        {
            response.Success = true;
            response.Message = string.IsNullOrEmpty(message) ? MessageHttpResponse.MESSAGE_SUCCESS : message;
            response.StatusCode = StatusCodes.Status200OK;
            response.Data = data;
            return response;
        }

        private async Task HandleExceptionAsync(Exception ex, string processName)
        {
            Error error = new()
            {
                Nameprocess = processName,
                Description = $"{ex.Message} - {ex.Source}",
                Ipcreation = "127.0.0.1",
                Usercreation = "system"
            };
            await _unitOfWork.Errors.SaveErrorsByProcedure(error);
        }
    }
}
