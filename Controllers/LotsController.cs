using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nastrafarmapi.DTOs.Moviments.Lots;
using nastrafarmapi.Extensions;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/{farmId:int}/lots")]
    public class LotsController : ControllerBase
    {
        private readonly ILotService service;

        public LotsController(ILotService lotServices)
        {
            service = lotServices;
        }

        /// <summary>
        /// Crea un nou lot per a una granja.
        /// </summary>
        /// <param name="farmId">ID de la granja.</param>
        /// <param name="dto">Dades del lot a crear.</param>
        /// <returns>Lot creat o errors de validació.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateLot(int farmId, [FromBody] CreateLotDTO dto)
        {
            var userId = User.GetUserId();
            var result = await service.CreateLotAsync(farmId, userId, dto);
            if (!result.Success) return BadRequest(new { result.Errors });
            return CreatedAtAction("GetLotById", new { farmId, id = result.Data }, result.Data);
        }

        /// <summary>
        /// Obtén tots els lots actius d'una granja.
        /// </summary>
        /// <param name="farmId">ID de la granja.</param>
        /// <returns>Llista de lots actius.</returns>
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveLots(int farmId)
        {
            var result = await service.GetActiveLotsByFarmAsync(farmId);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        /// <summary>
        /// Obtén tots els lots d'una granja.
        /// </summary>
        /// <param name="farmId">ID de la granja.</param>
        /// <returns>Llista de lots.</returns>
        [HttpGet]
        public async Task<IActionResult> GetLots(int farmId)
        {
            var result = await service.GetLotsByFarmIdAsync(farmId);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        /// <summary>
        /// Obtén un lot per ID dins d'una granja.
        /// </summary>
        /// <param name="farmId">ID de la granja.</param>
        /// <param name="id">ID del lot.</param>
        /// <returns>Lot trobat o error.</returns>
        [HttpGet("{id:int}", Name = "GetLotById")]
        public async Task<IActionResult> GetLotById(int farmId, int id)
        {
            var result = await service.GetLotByFarmAndIdAsync(farmId, id);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        /// <summary>
        /// Actualitza un lot existent.
        /// </summary>
        /// <param name="farmId">ID de la granja.</param>
        /// <param name="id">ID del lot.</param>
        /// <param name="dto">Dades noves del lot.</param>
        /// <returns>Lot actualitzat o errors.</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLot(int farmId, int id, [FromBody] UpdateLotDTO dto)
        {
            var userId = User.GetUserId();
            var result = await service.UpdateLotAsync(farmId, userId, dto);
            if (!result.Success) return BadRequest(new { result.Errors });
            return CreatedAtAction("GetLotById", new { farmId, id }, result.Data);
        }
    }
}
