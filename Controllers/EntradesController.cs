using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nastrafarmapi.DTOs.Moviments.Entrades;
using nastrafarmapi.Extensions;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Controllers
{

    [Authorize(Policy = "Entrades")]
    [ApiController]
    [Route("api/{farmId:int}/entrades")]
    public class EntradesController : ControllerBase
    {
        private readonly IEntradaService service;

        public EntradesController(IEntradaService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntrades(int farmId)
        {
            var result = await service.GetEntradesAsync(farmId);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        [HttpGet("{id:int}", Name = "GetEntradaById")]
        public async Task<IActionResult> GetEntradaById(int farmId, int id)
        {
            var result = await service.GetEntradaByIdAsync(farmId, id);
            if (!result.Success) return BadRequest(new { result.Errors });
            if (result.Data == null) return NotFound();
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntrada(int farmId, [FromBody] CreateEntradaDTO dto)
        {
            var userId = User.GetUserId();
            var result = await service.CreateEntradaAsync(farmId, userId, dto);
            if (!result.Success) return BadRequest(new { result.Errors });
            return CreatedAtAction("GetEntradaById", new { farmId, id = result.Data }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEntrada(int farmId, int id, [FromBody] UpdateEntradaDTO dto)
        {
            var result = await service.UpdateEntradaAsync(farmId, id, dto);
            if (!result.Success) return BadRequest(new { result.Errors });
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEntrada(int farmId, int id)
        {
            var result = await service.DeleteEntradaAsync(farmId, id);
            if (!result.Success) return BadRequest(new { result.Errors });
            return NoContent();
        }
    }
}
