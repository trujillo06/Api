using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TortilleriaSucursales.Dtos;
using TortilleriaSucursales.Services;

namespace TortilleriaSucursales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalService _service;

        public SucursalesController(ISucursalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllSucursales());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sucursal = await _service.GetSucursalById(id);
            return sucursal == null ? NotFound() : Ok(sucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SucursalDTO sucursalDto)
        {
            await _service.AddSucursal(sucursalDto);
            return CreatedAtAction(nameof(GetById), new { id = sucursalDto.Id_Sucursal }, sucursalDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SucursalDTO sucursalDto)
        {
            await _service.UpdateSucursal(id, sucursalDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteSucursal(id);
            return NoContent();
        }

    }
}
