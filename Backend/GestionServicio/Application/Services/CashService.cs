using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Utility.Static;

namespace Application.Services
{
    public class CashService : ICashService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CashService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponse<bool>> CreateCashAsync(CashRequest cashRequest)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var userExists = await ValidateUserAsync(cashRequest.UserId);
                if (userExists == null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);
                }

                if (userExists.RolRolid != (int)UserRole.Administrador && userExists.RolRolid != (int)UserRole.Gestor)
                {
                    return UnauthorizedResponse(response, MessageHttpResponse.MESSAGE_UNAUTHORIZED_SAVE);
                }

                var cashRegister = _mapper.Map<Cash>(cashRequest);
                cashRegister.Active = "N";
                cashRegister.Datecreation = DateTime.Now;
                var result = await _unitOfWork.Cash.SaveAsync(cashRegister);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status400BadRequest);
                }

                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "CreateCashAsync");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> UpdateCashAsync(CashRequest cashRequest, int cashId)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var cashExists = await _unitOfWork.Cash.GetCashByIdAsync(cashId);
                if (cashExists == null)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_REGISTER, StatusCodes.Status404NotFound);

                var userExists = await ValidateUserAsync(cashRequest.UserId);
                if (userExists == null) return response;

                if (userExists.RolRolid != (int)UserRole.Administrador)
                {
                    return UnauthorizedResponse(response, MessageHttpResponse.MESSAGE_UNAUTHORIZED_UPDATE);
                }

                var cashUpdate = _mapper.Map<Cash>(cashRequest);
                cashUpdate.Cashid = cashId;
                cashUpdate.Datecreation = cashExists.Datecreation;
                cashUpdate.Dateupdate = DateTime.Now;
                var result = await _unitOfWork.Cash.UpdateAsync(cashUpdate);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_UPDATE_REGISTER, StatusCodes.Status400BadRequest);
                }

                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "UpdateCashAsync");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<CashResponse>> GetCashByIdAsync(int cashId, int userId)
        {
            var response = new GenericResponse<CashResponse>();
            try
            {
                var cashExists = await _unitOfWork.Cash.GetCashByIdAsync(cashId);
                if (cashExists == null)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_REGISTER, StatusCodes.Status404NotFound);

                var userExists = await ValidateUserAsync(userId);
                if (userExists == null)
                    return response;

                return SuccessResponse(response, _mapper.Map<CashResponse>(cashExists), MessageHttpResponse.MESSAGE_SUCCESS);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetCashByIdAsync");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<DataResponse<CashResponse>>> GetListCashesAsync(FiltersRequest request)
        {
            var response = new GenericResponse<DataResponse<CashResponse>>();
            try
            {
                var cashes = await _unitOfWork.Cash.GetListCashesAsync(request);
                if (cashes == null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                }
                return SuccessResponse(response, _mapper.Map<DataResponse<CashResponse>>(cashes), MessageHttpResponse.MESSAGE_SUCCESS);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetListCashesAsync");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> AsignateUserCash(CashAsignateRequest cashAsignateRequest)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var userExists = await ValidateUserAsync(cashAsignateRequest.UserId);
                var userAuth = await ValidateUserAsync(cashAsignateRequest.UserIdAuth);
                if (userExists == null || userAuth == null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_REGISTER, StatusCodes.Status404NotFound);
                }

                if (userExists.Userapproval == null && userExists.Dateapproval == null)
                {
                    return UnauthorizedResponse(response, "No puede asignar a un usuario que aun no está aprobado");
                }

                if (userAuth.RolRolid == (int)UserRole.Cliente || userAuth.RolRolid == (int)UserRole.Cajero)
                {
                    return UnauthorizedResponse(response, MessageHttpResponse.MESSAGE_NOT_ASIGNATE_CASH_USER);
                }

                if (userExists.RolRolid != (int)UserRole.Cajero)
                {
                    return UnauthorizedResponse(response, MessageHttpResponse.MESSAGE_NOT_ASIGNATE_USER_CASH);
                }

                if (userExists.UserstatusStatusid != (int)UserState.Activo)
                {
                    return UnauthorizedResponse(response, $"No es posible asignar al usuario {userExists.Username} porque está en estado Inactivo");
                }

                var cash = await _unitOfWork.Usercash.GetUsercashByCashIdAsync(cashAsignateRequest.CashId);
                if (cash.TotalRecords > 1)
                {
                    return UnauthorizedResponse(response, MessageHttpResponse.MESSAGE_NOT_ASIGNATE_CASH_SIZE);
                }

                var cashUser = await _unitOfWork.Usercash.GetListUsercashsByUserAndByCashIdAsync(cashAsignateRequest.UserId, cashAsignateRequest.CashId);
                if (cashUser.TotalRecords > 0)
                {
                    return UnauthorizedResponse(response, $"No es posible asignar al usuario {userExists.Username} porque ya tiene asignada una caja");
                }

                var usercash = new Usercash
                {
                    CashCashid = cashAsignateRequest.CashId,
                    UserUserid = cashAsignateRequest.UserId,
                    Datecreation = DateTime.Now
                };

                var result = await _unitOfWork.Usercash.SaveAsync(usercash);
                if (!result)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status400BadRequest);
                }

                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "AsignateUserCash");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<User?> ValidateUserAsync(int userId)
        {
            if (userId == 0)
            {
                return null;
            }

            var userExists = await _unitOfWork.User.GetUserByIdAsync(userId);
            if (userExists == null)
            {
                return null;
            }

            return userExists;
        }

        private GenericResponse<T> ErrorResponse<T>(GenericResponse<T> response, string message, int statusCode)
        {
            response.Success = false;
            response.Message = message;
            response.StatusCode = statusCode;
            return response;
        }

        private GenericResponse<T> UnauthorizedResponse<T>(GenericResponse<T> response, string message)
        {
            response.Success = false;
            response.Message = message;
            response.StatusCode = StatusCodes.Status401Unauthorized;
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
