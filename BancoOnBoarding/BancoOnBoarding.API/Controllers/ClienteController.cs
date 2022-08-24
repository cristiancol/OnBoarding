using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BancoOnBoarding.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;
        private readonly IAsignacionClienteService _asignacionClienteService;

        public ClienteController(IAsignacionClienteService asignacionClienteService, 
            IClienteService clienteService)
        {
            _asignacionClienteService = asignacionClienteService;
            _service = clienteService;
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
        public IActionResult Create(ClienteDTO cliente)
        {
            _service.Crear(cliente);
            return Ok("Cliente creado exitosamente");
        }

        [HttpPut]
        public IActionResult Update(ClienteDTO cliente)
        {
            _service.Actualizar(cliente);
            return Ok("Cliente actualizado exitosamente.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Borrar(id);
            return Ok();
        }

        [HttpPost]
        [Route("AsignarPatio")]
        public IActionResult AsignarPatio([FromBody]AsignacionClienteDTO asignacionCliente) 
        {
            _asignacionClienteService.Asignar(asignacionCliente);
            return Ok("Cliente asignado correctamente.");
        }
    }
}
