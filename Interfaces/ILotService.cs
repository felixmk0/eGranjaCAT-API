using nastrafarmapi.DTOs.Moviments.Lots;
using nastrafarmapi.Services;

namespace nastrafarmapi.Interfaces
{
    public interface ILotService
    {
        Task<ServiceResult<bool>> CreateLotAsync(int farmId, int userId, CreateLotDTO createLotDTO);
        Task<ServiceResult<List<Lot>>> GetActiveLotsByFarmAsync(int farmId);
        Task<ServiceResult<Lot>> GetLotByFarmAndIdAsync(int farmId, int lotId);
        Task<ServiceResult<Lot>> GetLotsByFarmIdAsync(int farmId);
        Task<ServiceResult<bool>> UpdateLotAsync(int farmId, int lotId, UpdateLotDTO dto);
    }
}