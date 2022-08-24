using BancoOnBoarding.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BancoOnBoarding.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargaInicialController : ControllerBase
    {
        private readonly ICargaDatosEjecutivoService _service;
        private readonly ICargaDatosClienteService _clienteService;
        private readonly ICargaDatosMarcaService _marcaService;

        public CargaInicialController(ICargaDatosEjecutivoService service,
            ICargaDatosClienteService clienteService,
            ICargaDatosMarcaService marcaService)
        {
            _service = service;
            _clienteService = clienteService;
            _marcaService = marcaService;
        }

        [HttpPost]
        public IActionResult Cargar()
        {
            _service.Cargar();
            _clienteService.Cargar();
            _marcaService.Cargar();
            return Ok("Carga realizada exitosamente");
        }
    }
}
