using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nastrafarmapi.Data;
using nastrafarmapi.DTOs.Moviments.Lots;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Services
{
    public class LotService : ILotService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public LotService(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResult<bool>> CreateLotAsync(int farmId, int userId, CreateLotDTO createLotDTO)
        {
            var resultObj = new ServiceResult<bool>();
            var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);

            if (!farmExists)
            {
                resultObj.Success = false;
                resultObj.Errors.Add("La granja no existe.");
                return resultObj;
            }

            var lot = mapper.Map<Lot>(createLotDTO);
            lot.FarmId = farmId;
            lot.CreatedBy = userId.ToString();

            await context.Lots.AddAsync(lot);
            await context.SaveChangesAsync();

            resultObj.Success = true;
            resultObj.Data = true;
            return resultObj;
        }

        public async Task<ServiceResult<List<Lot>>> GetActiveLotsByFarmAsync(int farmId)
        {
            var resultObj = new ServiceResult<List<Lot>>();
            var lots = await context.Lots.Where(l => l.FarmId == farmId && l.Active).ToListAsync();

            if (lots == null || !lots.Any())
            {
                resultObj.Success = false;
                resultObj.Errors.Add("No hi ha lots actius per a aquesta granja.");
                return resultObj;
            }
            resultObj.Success = true;
            resultObj.Data = lots;
            return resultObj;
        }

        public async Task<ServiceResult<Lot>> GetLotByFarmAndIdAsync(int farmId, int lotId)
        {
            var resultObj = new ServiceResult<Lot>();
            var lot = await context.Lots.FirstOrDefaultAsync(l => l.Id == lotId && l.FarmId == farmId);

            if (lot == null)
            {
                resultObj.Success = false;
                resultObj.Errors.Add("Lot no trobat");
                return resultObj;
            }

            resultObj.Success = true;
            resultObj.Data = lot;
            return resultObj;
        }

        public async Task<ServiceResult<Lot>> GetLotsByFarmIdAsync(int farmId)
        {
            var resultObj = new ServiceResult<Lot>();
            var lot = await context.Lots.FirstOrDefaultAsync(l => l.FarmId == farmId);

            if (lot == null)
            {
                resultObj.Success = false;
                resultObj.Errors.Add("Lots no trobats");
                return resultObj;
            }

            resultObj.Success = true;
            resultObj.Data = lot;
            return resultObj;
        }

        public async Task<ServiceResult<bool>> UpdateLotAsync(int farmId, int lotId, UpdateLotDTO dto)
        {
            var result = new ServiceResult<bool>();
            var lot = await context.Lots.FirstOrDefaultAsync(l => l.Id == lotId && l.FarmId == farmId);

            if (lot == null)
            {
                result.Success = false;
                result.Errors.Add("Lot no trobat");
                return result;
            }

            mapper.Map(dto, lot);
            lot.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            result.Success = true;
            result.Data = true;
            return result;
        }
    }
}
