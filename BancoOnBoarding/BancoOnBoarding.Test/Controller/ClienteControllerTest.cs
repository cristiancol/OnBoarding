using BancoOnBoarding.API.Controllers;
using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BancoOnBoarding.Test.Controller
{
    public class ClienteControllerTest
    {
        [Fact]
        public void AsignarPatio_AsignacionExitosa_Returns200() 
        {
            Mock<IAsignacionClienteService> service = new Mock<IAsignacionClienteService>();
            Mock<IClienteService> clienteService = new Mock<IClienteService>();

            ClienteController controller = new ClienteController(service.Object,
                clienteService.Object);
            var result = controller.AsignarPatio(new AsignacionClienteDTO());
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void AsignarPatio_AsignacionFallida_LanzaExcepcion()
        {
            Mock<IAsignacionClienteService> service = new Mock<IAsignacionClienteService>();
            Mock<IClienteService> clienteService = new Mock<IClienteService>();

            service.Setup(s => s.Asignar(It.IsAny<AsignacionClienteDTO>())).Throws(new BancoOnBoardingException("Exception"));

            ClienteController controller = new ClienteController(service.Object,
                clienteService.Object);

            Assert.Throws<BancoOnBoardingException>(() => controller.AsignarPatio(new AsignacionClienteDTO()));
        }
    }
}
