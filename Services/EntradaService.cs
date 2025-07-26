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

        public async Task<ServiceResult<int?>> CreateEntradaAsync(int farmId, int userId, CreateEntradaDTO createEntradaDTO)
        {
            var resultObj = new ServiceResult<int?>();
            try
            {
                var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);
                if (!farmExists)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("La granja no existeix");
                    return resultObj;
                }

                var lotExists = await context.Lots.AnyAsync(l => l.Id == createEntradaDTO.LotId && l.FarmId == farmId);
                if (!lotExists)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("El lot especificat no existeix");
                    return resultObj;
                }

                var entrada = mapper.Map<Entrada>(createEntradaDTO);
                entrada.FarmId = farmId;
                entrada.CreatedBy = userId.ToString();
                entrada.CreatedAt = DateTime.UtcNow;

                await context.Entrades.AddAsync(entrada);
                await context.SaveChangesAsync();

                resultObj.Success = true;
                resultObj.Data = entrada.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al crear l'entrada");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en crear l'entrada");
            }
            return resultObj;
        }

        public async Task<ServiceResult<List<GetEntradaDTO>>> GetEntradesAsync(int farmId)
        {
            var resultObj = new ServiceResult<List<GetEntradaDTO>>();
            try
            {
                var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);
                if (!farmExists)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("La granja no existeix");
                    return resultObj;
                }
                var entrades = await context.Entrades
                    .Where(e => e.FarmId == farmId)
                    .ToListAsync();
                if (entrades == null || !entrades.Any())
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("No s'han trobat entrades");
                    return resultObj;
                }
                var entradesDTOs = mapper.Map<List<GetEntradaDTO>>(entrades);
                resultObj.Data = entradesDTOs;
                resultObj.Success = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtenir les entrades");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en obtenir les entrades");
            }
            return resultObj;
        }

        public async Task<ServiceResult<GetEntradaDTO?>> GetEntradaByIdAsync(int farmId, int entradaId)
        {
            var resultObj = new ServiceResult<GetEntradaDTO?>();
            try
            {
                var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);
                if (!farmExists)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("La granja no existeix");
                    return resultObj;
                }
                var entrada = await context.Entrades
                    .Where(e => e.FarmId == farmId && e.Id == entradaId)
                    .FirstOrDefaultAsync();
                if (entrada == null)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("Entrada no trobada");
                    return resultObj;
                }
                var entradaDTO = mapper.Map<GetEntradaDTO>(entrada);
                resultObj.Data = entradaDTO;
                resultObj.Success = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtenir l'entrada");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en obtenir l'entrada");
            }
            return resultObj;
        }

        public async Task<ServiceResult<bool>> UpdateEntradaAsync(int farmId, int entradaId, UpdateEntradaDTO updateEntradaDTO)
        {
            var resultObj = new ServiceResult<bool>();
            try
            {
                var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);
                if (!farmExists)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("La granja no existeix");
                    return resultObj;
                }
                var entrada = await context.Entrades.Where(e => e.FarmId == farmId && e.Id == entradaId).FirstOrDefaultAsync();

                if (entrada == null)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("Entrada no trobada");
                    return resultObj;
                }

                mapper.Map(updateEntradaDTO, entrada);
                entrada.UpdatedAt = DateTime.UtcNow;
                context.Entrades.Update(entrada);
                await context.SaveChangesAsync();

                resultObj.Success = true;
                resultObj.Data = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al actualitzar l'entrada");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en actualitzar l'entrada");
            }
            return resultObj;
        }

        public async Task<ServiceResult<bool>> DeleteEntradaAsync(int farmId, int entradaId)
        {
            var resultObj = new ServiceResult<bool>();
            try
            {
                var farmExists = await context.Farms.AnyAsync(f => f.Id == farmId);
                if (!farmExists)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("La granja no existeix");
                    return resultObj;
                }
                var entrada = await context.Entrades.Where(e => e.FarmId == farmId && e.Id == entradaId).FirstOrDefaultAsync();

                if (entrada == null)
                {
                    resultObj.Success = false;
                    resultObj.Errors.Add("Entrada no trobada");
                    return resultObj;
                }
                context.Entrades.Remove(entrada);
                await context.SaveChangesAsync();

                resultObj.Success = true;
                resultObj.Data = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al eliminar l'entrada");
                resultObj.Success = false;
                resultObj.Errors.Add("Error inesperat en eliminar l'entrada");
            }
            return resultObj;
        }
    }

}
