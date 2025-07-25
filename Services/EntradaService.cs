using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nastrafarmapi.Data;
using nastrafarmapi.DTOs.Moviments.Entrades;
using nastrafarmapi.Entities.Moviments;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Services
{
    public class EntradaService : IEntradaService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly ILogger<EntradaService> logger;

        public EntradaService(IMapper mapper, ApplicationDbContext context, ILogger<EntradaService> logger)
        {
            this.mapper = mapper;
            this.context = context;
            this.logger = logger;
        }

        public async Task<ServiceResult<bool>> CreateEntradaAsync(int farmId, int userId, CreateEntradaDTO createEntradaDTO)
        {
            var resultObj = new ServiceResult<bool>();
            try
            {
                var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);
                if (!farmExists)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("La granja no existe.");
                    return resultObj;
                }

                var entrada = mapper.Map<Entrada>(createEntradaDTO);
                entrada.FarmId = farmId;
                entrada.CreatedBy = userId.ToString();
                entrada.CreatedAt = DateTime.UtcNow;

                await context.Entrades.AddAsync(entrada);
                await context.SaveChangesAsync();
                resultObj.Success = true;
                resultObj.Data = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear l'entrada");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en crear l'entrada");
            }
            return resultObj;
        }
    }
}
