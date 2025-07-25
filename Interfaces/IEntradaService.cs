using nastrafarmapi.DTOs.Moviments.Entrades;
using nastrafarmapi.Services;

namespace nastrafarmapi.Interfaces
{
    public interface IEntradaService
    {
        Task<ServiceResult<bool>> CreateEntradaAsync(int farmId, int userId, CreateEntradaDTO createEntradaDTO);
    }
}