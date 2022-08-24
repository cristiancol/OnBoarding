using BancoOnBoarding.API.Controllers;
using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BancoOnBoarding.Test.Controller
{
    public class SolicitudCreditoControllerTest
    {
        [Fact]
        public void Create_SolicitudExitosa_Returns200()
        {
            Mock<ISolicitudCreditoService> service = new Mock<ISolicitudCreditoService>();

            SolicitudCreditoController controller = new SolicitudCreditoController(service.Object);
            var result = controller.Create(new SolicitudCreditoDTO());
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void Create_SolicitudFallida_LanzaExcepcion()
        {
            Mock<ISolicitudCreditoService> service = new Mock<ISolicitudCreditoService>();

            service.Setup(s => s.Solicitar(It.IsAny<SolicitudCreditoDTO>())).Throws(new BancoOnBoardingException("Exception"));

            SolicitudCreditoController controller = new SolicitudCreditoController(service.Object);

            Assert.Throws<BancoOnBoardingException>(() => controller.Create(new SolicitudCreditoDTO()));
        }
    }
}
