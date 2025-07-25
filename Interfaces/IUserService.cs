using nastrafarmapi.DTOs;
using nastrafarmapi.DTOs.Users;
using nastrafarmapi.Services;

namespace nastrafarmapi.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<AuthResponseDTO>> CreateUserAsync(CreateUserDTO userDTO);
        Task<ServiceResult<bool>> DeleteUserById(int id);
        Task<ServiceResult<GetUserDTO?>> GetUserByIdAsync(int id);
        Task<ServiceResult<List<GetUserDTO>>> GetUsersAsync();
        Task<ServiceResult<AuthResponseDTO>> LoginUserAsync(LoginUserDTO loginDTO);
    }
}