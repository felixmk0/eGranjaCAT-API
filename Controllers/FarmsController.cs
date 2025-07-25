using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nastrafarmapi.DTOs.Farms;
using nastrafarmapi.Interfaces;

namespace nastrafarmapi.Controllers
{
    [ApiController]
    [Route("api/farms")]
    [Authorize]
    public class FarmsController : ControllerBase
    {
        private readonly IFarmService farmService;

        public FarmsController(IFarmService farmService)
        {
            this.farmService = farmService;
        }

        [HttpGet]

        public async Task<IActionResult> GetFarmsAsync()
        {
            var result = await farmService.GetFarmsAsync();
            if (!result.Success) return BadRequest(new { result.Errors });
            return Ok(result.Data);
        }

        [HttpGet("{id:int}", Name = "GetFarmById")]
        public async Task<IActionResult> GetFarmByIdAsync(int id)
        {
            var result = await farmService.GetFarmByIdAsync(id);
            if (!result.Success) return BadRequest(new { result.Errors });

            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFarmAsync([FromBody] CreateFarmDTO createFarmDTO)
        {
            var result = await farmService.CreateFarmAsync(createFarmDTO);
            if (!result.Success) return BadRequest(new { result.Errors });

            return CreatedAtRoute("GetFarmById", new { id = result.Data }, null);
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFarmAsync(int id)
        {
            var result = await farmService.DeleteFarmAsync(id);
            if (!result.Success) return BadRequest(new { result.Errors });

            return NoContent();
        }
    }
}
