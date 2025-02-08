using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using Application.Validations;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Commons.Reponse;
using Infraestructure.Commons.Request;
using Infraestructure.Presistences.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility.Static;
using Bct = BCrypt.Net.BCrypt;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserValidator _validations;
        
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, UserValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _validations = validationRules;
        }

        public async Task<GenericResponse<DataResponse<UserReponse>>> GetListUser(GenericRequest<FiltersRequest> request, int userAuthId)
        {
            var response = new GenericResponse<DataResponse<UserReponse>>();
            try
            {
                if(request.Value is null)
                {
                    return ErrorResponse(response, "No existe la clave 'Value' asegurate de que contenga datos", StatusCodes.Status404NotFound);
                }
                var userExists = await ValidateUserAsync(userAuthId);
                if (userExists is null)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_REGISTER, StatusCodes.Status404NotFound);
                }

                if (userExists.RolRolid == (int)UserRole.Cliente || userExists.RolRolid == (int)UserRole.Cajero)
                {
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_UNAUTHORIZED_ROLE, StatusCodes.Status401Unauthorized);
                }

                if (userExists.RolRolid == (int)UserRole.Gestor)
                {
                    request.Value!.NumFilter = 3;
                }

                var userList = await _unitOfWork.User.GetListUserAsync(request.Value!, userAuthId);
                if (userList is null)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                var mapUserList = _mapper.Map<DataResponse<UserReponse>>(userList);
                return SuccessResponse(response, mapUserList);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetListUser");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<string>> GetLoginToken(LoginRequest request)
        {
            var response = new GenericResponse<string>();
            try
            {
                var user = await _unitOfWork.User.GetUserByUserNameAsync(request.UserName!);
                if (user is null)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND, StatusCodes.Status404NotFound);
                if(user.Dateapproval == null)
                    return ErrorResponse(response, "Tu usuario aún no ha sido aprobado, por favor comunicate con el Administrador", StatusCodes.Status404NotFound);
                if (!Bct.Verify(request.Password, user!.Password))
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_UNAUTHORIZED, StatusCodes.Status401Unauthorized);
                return SuccessResponse(response, GetGeneretToken(user));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetLoginToken");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<UserReponse>> GetUserById(int id)
        {
            var response = new GenericResponse<UserReponse>();
            try
            {
                var userExists = await ValidateUserAsync(id);
                if (userExists is null)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);
                var mapUser = _mapper.Map<UserReponse>(userExists);
                return SuccessResponse(response, mapUser);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "GetUserById");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> CreateUser(UserRequest user)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var validationResult = await _validations.ValidateAsync(user);
                if (!validationResult.IsValid)
                {
                    response.Erros = validationResult.Errors;
                    return ErrorResponse(response, "Error por validación", StatusCodes.Status400BadRequest);
                }

                var userExists = await ValidateUserAsync(user.UserAuthId);
                if (userExists is null)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);

                var nameUserExists = await _unitOfWork.User.GetUserByUserNameAsync(user.Username);
                if (nameUserExists is not null)
                    return ErrorResponse(response, "El nombre de usuario ya existe, en el registro, porfavor registra otro usuario", StatusCodes.Status400BadRequest);

                if (!CanCreateUser(userExists!.RolRolid, user.Rolid))
                    return ErrorResponse(response, "No tienes permisos para crear este tipo de usuario.", StatusCodes.Status400BadRequest);

                var userEntity = _mapper.Map<User>(user);
                userEntity.Userid = 0;
                userEntity.Usercreate = user.UserAuthId;
                userEntity.Password = Bct.HashPassword(user.Password);
                userEntity.Datecreation = DateTime.Now;
                userEntity.Creationdate = DateTime.Now;

                var userCreate = await _unitOfWork.User.SaveAsync(userEntity);
                if (!userCreate)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_SAVE_REGISTER, StatusCodes.Status400BadRequest);
                return SuccessResponse(response, true);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "CreateUser");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<GenericResponse<bool>> ApproveUser(int userId, int userAuthId)
        {
            var response = new GenericResponse<bool>();
            try
            {
                var currentUser = await ValidateUserAsync(userAuthId);
                if (currentUser is null)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_NOT_FOUND_USER, StatusCodes.Status404NotFound);

                if (currentUser!.RolRolid != (int)UserRole.Administrador)
                    return ErrorResponse(response, "No tienes permisos aprovar este usuario", StatusCodes.Status401Unauthorized);

                var result = await _unitOfWork.User.ApproveUserAsync(userId, userAuthId);
                if (!result)
                    return ErrorResponse(response, MessageHttpResponse.MESSAGE_ERROR, StatusCodes.Status500InternalServerError);
                return SuccessResponse(response, true, "Usuario aprovado correctamente");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, "ApproveUser");
                return ErrorResponse(response, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private string GetGeneretToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Userid.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.RolRol.Rolname),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64)
                };
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"])),
                signingCredentials: signinCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private bool CanCreateUser(int creatorRoleId, int newUserRoleId)
        {
            if (creatorRoleId == (int)UserRole.Administrador)
                return newUserRoleId == (int)UserRole.Administrador || newUserRoleId == (int)UserRole.Gestor || newUserRoleId == (int)UserRole.Cajero;
            else if (creatorRoleId == (int)UserRole.Gestor)
                return newUserRoleId == (int)UserRole.Cajero;
            else if (creatorRoleId == (int)UserRole.Cajero)
                return false;
            return false;
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
            response.StatusMessage = HttpStatus.MS_ERROR;
            return response;
        }

        private GenericResponse<T> SuccessResponse<T>(GenericResponse<T> response, T data, string message = "")
        {
            response.Success = true;
            response.Message = string.IsNullOrEmpty(message) ? MessageHttpResponse.MESSAGE_SUCCESS : message;
            response.StatusCode = StatusCodes.Status200OK;
            response.StatusMessage =  HttpStatus.MS_OK;
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
