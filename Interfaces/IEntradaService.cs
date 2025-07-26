using nastrafarmapi.DTOs.Moviments.Entrades;
using nastrafarmapi.Services;

namespace nastrafarmapi.Interfaces
{
    public interface IEntradaService
    {
        Task<ServiceResult<int?>> CreateEntradaAsync(int farmId, int userId, CreateEntradaDTO createEntradaDTO);
        Task<ServiceResult<bool>> DeleteEntradaAsync(int farmId, int entradaId);
        Task<ServiceResult<GetEntradaDTO?>> GetEntradaByIdAsync(int farmId, int entradaId);
        Task<ServiceResult<List<GetEntradaDTO>>> GetEntradesAsync(int farmId);
        Task<ServiceResult<bool>> UpdateEntradaAsync(int farmId, int entradaId, UpdateEntradaDTO updateEntradaDTO);
    }
}