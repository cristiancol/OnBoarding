using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BancoOnBoarding.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolicitudCreditoController : ControllerBase
    {
        private readonly ISolicitudCreditoService _solicitudCreditoService;
        public SolicitudCreditoController(ISolicitudCreditoService solicitudCreditoService)
        {
            _solicitudCreditoService = solicitudCreditoService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SolicitudCreditoDTO solicitud)
        {
            _solicitudCreditoService.Solicitar(solicitud);
            return Ok("Solicitud realizada correctamente.");
        }

        [HttpPut]
        [Route("Despachar/{id}")]
        public IActionResult Despachar(int id)
        {
            _solicitudCreditoService.Despachar(id);
            return Ok("Solicitud despachada correctamente.");
        }

        [HttpPut]
        [Route("Cancelar/{id}")]
        public IActionResult Cancelar(int id)
        {
            _solicitudCreditoService.Cancelar(id);
            return Ok("Solicitud cancelada correctamente.");
        }
    }
}
