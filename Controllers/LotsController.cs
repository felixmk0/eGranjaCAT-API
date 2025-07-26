using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nastrafarmapi.DTOs.Moviments.Lots;
using nastrafarmapi.Extensions;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Controllers
{

    [Authorize(Policy = "Lots")]
    [ApiController]
    [Route("api/{farmId:int}/lots")]
    public class LotsController : ControllerBase
    {
        private readonly ILotService service;

        public LotsController(ILotService lotServices)
        {
            service = lotServices;
        }


        [HttpPost]
        public async Task<IActionResult> CreateLot(int farmId, [FromBody] CreateLotDTO dto)
        {
            var userId = User.GetUserId();
            var result = await service.CreateLotAsync(farmId, userId, dto);
            if (!result.Success) return BadRequest(new { result.Errors });
            return CreatedAtAction("GetLotById", new { farmId, id = result.Data }, result.Data);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveLots(int farmId)
        {
            var result = await service.GetActiveLotsByFarmAsync(farmId);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetLots(int farmId)
        {
            var result = await service.GetLotsByFarmIdAsync(farmId);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        [HttpGet("{id:int}", Name = "GetLotById")]
        public async Task<IActionResult> GetLotById(int farmId, int id)
        {
            var result = await service.GetLotByFarmAndIdAsync(farmId, id);
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

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
