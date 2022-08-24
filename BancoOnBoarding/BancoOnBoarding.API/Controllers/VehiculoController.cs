using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BancoOnBoarding.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiculoController : ControllerBase
    {

        private readonly IVehiculoService _service;
        public VehiculoController(IVehiculoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.Obtener());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_service.Obtener(id));
        }

        [HttpPost]
        public IActionResult Create(VehiculoDTO cliente)
        {
            _service.Crear(cliente);
            return Ok("Vehiculo creado exitosamente");
        }

        [HttpPut]
        public IActionResult Update(VehiculoDTO cliente)
        {
            _service.Actualizar(cliente);
            return Ok("Vehiculo actualizado exitosamente.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Borrar(id);
            return Ok();
        }
    }
}
