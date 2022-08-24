using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BancoOnBoarding.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatioController : ControllerBase
    {
        private readonly IPatioService _service;

        public PatioController(IPatioService service)
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
        public IActionResult Create(PatioDTO patio)
        {
            _service.Crear(patio);
            return Ok("Patio creado exitosamente");
        }

        [HttpPut]
        public IActionResult Update(PatioDTO patio)
        {
            _service.Actualizar(patio);
            return Ok("Patio actualizado exitosamente.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Borrar(id);
            return Ok();
        }
    }
}
