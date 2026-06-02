using Application.DTOs.UsrDTOs;
using Application.IOC.Interface;
using Grpc.Core;
using WebApis.Protos;
namespace WebApis.GRPC
{
    public class UserGrpcService : WebApis.Protos.UserService.UserServiceBase
    {
        private readonly IUserService _userService;

        public UserGrpcService(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task<GetAllUsersResponse> GetAll(GetAllUsersRequest request, ServerCallContext context)
        {
            var result = await _userService.GetAllAsync();
            var response = new GetAllUsersResponse();

            if (result.IsSuccess && result.Data is not null)
            {
                response.Users.AddRange(result.Data.Select(MapToMessage));
            }

            return response;
        }

        public override async Task<UserResponse> GetById(GetUserByIdRequest request, ServerCallContext context)
        {
            var result = await _userService.GetByIdAsync(request.Id);

            if (!result.IsSuccess)
                return new UserResponse { Success = false, ErrorMessage = result.ErrorMessage };

            return new UserResponse { Success = true, User = MapToMessage(result.Data!) };
        }

        public override async Task<UserResponse> Create(CreateUserRequest request, ServerCallContext context)
        {
            var dto = new CreateUserDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                BirthDate = DateTime.Parse(request.BirthDate)
            };

            var result = await _userService.CreateAsync(dto);

            if (!result.IsSuccess)
                return new UserResponse { Success = false, ErrorMessage = result.ErrorMessage };

            return new UserResponse { Success = true, User = MapToMessage(result.Data!) };
        }

        public override async Task<UserResponse> Update(UpdateUserRequest request, ServerCallContext context)
        {
            var dto = new UpdateUserDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode,
                BirthDate = DateTime.Parse(request.BirthDate)
            };

            var result = await _userService.UpdateAsync(request.Id, dto);

            if (!result.IsSuccess)
                return new UserResponse { Success = false, ErrorMessage = result.ErrorMessage };

            return new UserResponse { Success = true, User = MapToMessage(result.Data!) };
        }

        public override async Task<DeleteUserResponse> Delete(DeleteUserRequest request, ServerCallContext context)
        {
            var result = await _userService.DeleteAsync(request.Id);

            if (!result.IsSuccess)
                return new DeleteUserResponse { Success = false, ErrorMessage = result.ErrorMessage };

            return new DeleteUserResponse { Success = true };
        }

        private static UserMessage MapToMessage(UserDto dto)
        {
            return new UserMessage
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NationalCode = dto.NationalCod,
                BirthDate = dto.BirthDate.ToString("yyyyMMdd")
            };
        }
    }
}
