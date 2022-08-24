using BancoOnBoarding.API.Controllers;
using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BancoOnBoarding.Test.Controller
{
    public class PatioControllerTest
    {
        private Mock<IPatioService> _service; 
        public PatioControllerTest()
        {
            _service = new Mock<IPatioService>();
        }

        [Fact]
        public void Get_ObtenerPatioNoExistente_RetornaNulo() 
        {
            PatioDTO patio = null;
            _service.Setup(s => s.Obtener(It.IsAny<int>())).Returns(patio);

            PatioController controller = new PatioController(_service.Object);
            var result = controller.Get(1);
            var okResult = result as OkObjectResult;

            PatioDTO? patioResultado = okResult?.Value as PatioDTO;

            Assert.Null(patioResultado);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void Get_ObtenerPatioExistente_RetornaPatio()
        {
            PatioDTO patio = new PatioDTO();
            _service.Setup(s => s.Obtener(It.IsAny<int>())).Returns(patio);

            PatioController controller = new PatioController(_service.Object);
            var result = controller.Get(1);
            var okResult = result as OkObjectResult;

            PatioDTO? patioResultado = okResult?.Value as PatioDTO;

            Assert.NotNull(patioResultado);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void Get_ObtenerPatiosSinPatios_RetornaListaVacia()
        {
            _service.Setup(s => s.Obtener()).Returns(new List<PatioDTO>());

            PatioController controller = new PatioController(_service.Object);
            var result = controller.Get();
            var okResult = result as OkObjectResult;

            List<PatioDTO>? patiosResultado = okResult?.Value as List<PatioDTO>;

            Assert.False(patiosResultado?.Any());
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void Get_ObtenerPatios_RetornaListaPatios()
        {
            List<PatioDTO> patios = new List<PatioDTO>();
            patios.Add(new PatioDTO());
            _service.Setup(s => s.Obtener()).Returns(patios);

            PatioController controller = new PatioController(_service.Object);
            var result = controller.Get();
            var okResult = result as OkObjectResult;

            List<PatioDTO>? patiosResultado = okResult?.Value as List<PatioDTO>;

            Assert.True(patiosResultado?.Any());
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void Create_CreaPatio()
        {
            _service.Setup(s => s.Crear(It.IsAny<PatioDTO>())).Verifiable();

            PatioController controller = new PatioController(_service.Object);
            var result = controller.Create(new PatioDTO());
            var okResult = result as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
            _service.Verify(s => s.Crear(It.IsAny<PatioDTO>()), Times.Once);
        }

        [Fact]
        public void Update_ActualizaPatio()
        {
            _service.Setup(s => s.Actualizar(It.IsAny<PatioDTO>())).Verifiable();

            PatioController controller = new PatioController(_service.Object);
            var result = controller.Update(new PatioDTO());
            var okResult = result as OkObjectResult;

            Assert.Equal(200, okResult?.StatusCode);
            _service.Verify(s => s.Actualizar(It.IsAny<PatioDTO>()), Times.Once);
        }
    }
}
