using nastrafarmapi.DTOs.Farms;
using nastrafarmapi.Services;

namespace nastrafarmapi.Interfaces
{
    public interface IFarmService
    {
        Task<ServiceResult<List<GetFarmDTO>>> GetFarmsAsync();
        Task<ServiceResult<int?>> CreateFarmAsync(CreateFarmDTO createFarmDTO);
        Task<ServiceResult<GetFarmDTO?>> GetFarmByIdAsync(int id);
        Task<ServiceResult<bool>> DeleteFarmAsync(int id);
    }
}
