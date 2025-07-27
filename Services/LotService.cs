using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using nastrafarmapi.Data;
using nastrafarmapi.DTOs.Moviments.Lots;
using nastrafarmapi.Entities;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Services
{
    public class LotService : ILotService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public LotService(UserManager<User> userManager, IMapper mapper, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResult<int?>> CreateLotAsync(int farmId, string userId, CreateLotDTO createLotDTO)
        {
            var resultObj = new ServiceResult<int?>();

            var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);
            if (!farmExists)
            {
                resultObj.Success = false;
                resultObj.Errors.Add("La granja no existe.");
                return resultObj;
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                resultObj.Success = false;
                resultObj.Errors.Add("Usuari no vàlid.");
                return resultObj;
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                resultObj.Success = false;
                resultObj.Errors.Add("Usuari no trobat.");
                return resultObj;
            }

            var lot = mapper.Map<Lot>(createLotDTO);
            lot.FarmId = farmId;
            lot.UserGuid = user.Id;
            lot.CreatedAt = DateTime.UtcNow;

            await context.Lots.AddAsync(lot);
            await context.SaveChangesAsync();

            resultObj.Success = true;
            resultObj.Data = lot.Id;
            return resultObj;
        }


        public async Task<ServiceResult<List<GetLotDTO>>> GetActiveLotsByFarmAsync(int farmId)
        {
            var resultObj = new ServiceResult<List<GetLotDTO>>();
            var lots = await context.Lots.Include(l => l.User).Include(l => l.Farm).Where(l => l.FarmId == farmId && l.Active).ToListAsync();
           
            if (lots == null || !lots.Any())
            {
                resultObj.Success = false;
                resultObj.Errors.Add("No hi ha lots actius per a aquesta granja.");
                return resultObj;
            }

            var lotsDTO = mapper.Map<List<GetLotDTO>>(lots);

            resultObj.Success = true;
            resultObj.Data = lotsDTO;
            return resultObj;
        }

        public async Task<ServiceResult<GetLotDTO>> GetLotByFarmAndIdAsync(int farmId, int lotId)
        {
            var resultObj = new ServiceResult<GetLotDTO>();
            var lot = await context.Lots.Include(l => l.User).Include(l => l.Farm).FirstOrDefaultAsync(l => l.Id == lotId && l.FarmId == farmId);

            if (lot == null)
            {
                resultObj.Success = false;
                resultObj.Errors.Add("Lot no trobat");
                return resultObj;
            }

            var lotDTO = mapper.Map<GetLotDTO>(lot);

            resultObj.Success = true;
            resultObj.Data = lotDTO;
            return resultObj;
        }

        public async Task<ServiceResult<GetLotDTO>> GetLotsByFarmIdAsync(int farmId)
        {
            var resultObj = new ServiceResult<GetLotDTO>();
            var lot = await context.Lots.Include(l => l.User).Include(l => l.Farm).FirstOrDefaultAsync(l => l.FarmId == farmId);

            if (lot == null)
            {
                resultObj.Success = false;
                resultObj.Errors.Add("Lots no trobats");
                return resultObj;
            }

            var lotDTO = mapper.Map<GetLotDTO>(lot);

            resultObj.Success = true;
            resultObj.Data = lotDTO;
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
