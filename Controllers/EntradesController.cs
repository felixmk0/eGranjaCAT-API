using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nastrafarmapi.DTOs.Moviments.Entrades;
using nastrafarmapi.Extensions;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/{farmId:int}/entrades")]
    public class EntradesController : ControllerBase
    {
        private readonly IEntradaService service;

        public EntradesController(IEntradaService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntrada(int farmId, [FromBody] CreateEntradaDTO dto)
        {
            var userId = User.GetUserId();
            var result = await service.CreateEntradaAsync(farmId, userId, dto);
            if (!result.Success) return BadRequest(new { result.Errors });
            return NoContent();
            //return CreatedAtAction("GetEntradaById", new { farmId, id = result.Data }, result.Data);
        }
    }
}
