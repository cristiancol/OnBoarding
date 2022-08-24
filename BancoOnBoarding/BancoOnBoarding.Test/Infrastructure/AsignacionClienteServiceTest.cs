using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Infrastructure.Services;
using BancoOnBoarding.Repository.Interfaces;
using Moq;

namespace BancoOnBoarding.Test.Infrastructure
{
    public class AsignacionClienteServiceTest
    {
        private Mock<IAsignacionClienteRepository> asociacionClienteRepository;
        private Mock<IClienteRepository> clienteRepository;
        private Mock<IPatioRepository> patioRepository;
        public AsignacionClienteServiceTest()
        {
            asociacionClienteRepository = new Mock<IAsignacionClienteRepository>();
            clienteRepository = new Mock<IClienteRepository>();
            patioRepository = new Mock<IPatioRepository>();
        }

        [Fact]
        public void Asignar_ClienteNoExiste_LanzaExcepcion()
        {
            Cliente cliente = null;
            clienteRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(cliente);
            patioRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Patio());

            AsignacionClienteDTO dto = new AsignacionClienteDTO();
            AsignacionClienteService asignacionCliente = new AsignacionClienteService(asociacionClienteRepository.Object, clienteRepository.Object, patioRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => asignacionCliente.Asignar(dto));
        }

        [Fact]
        public void Asignar_PatioNoExiste_LanzaExcepcion()
        {
            Patio patio = null;
            clienteRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Cliente());
            patioRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);

            AsignacionClienteDTO dto = new AsignacionClienteDTO();

            AsignacionClienteService asignacionCliente = new AsignacionClienteService(asociacionClienteRepository.Object, clienteRepository.Object, patioRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => asignacionCliente.Asignar(dto));
        }

        [Fact]
        public void Asignar_SinAsociacion_AsociacionExitosa()
        {
            clienteRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Cliente());
            patioRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Patio());
            asociacionClienteRepository.Setup(x => x.ObtenerAsociacion(It.IsAny<int>())).Returns(new List<AsignacionCliente>());
            asociacionClienteRepository.Setup(x => x.Add(It.IsAny<AsignacionCliente>())).Verifiable();

            AsignacionClienteDTO dto = new AsignacionClienteDTO();

            AsignacionClienteService asignacionCliente = new AsignacionClienteService(asociacionClienteRepository.Object, clienteRepository.Object, patioRepository.Object);
            asignacionCliente.Asignar(dto);

            asociacionClienteRepository.Verify(x => x.Add(It.IsAny<AsignacionCliente>()), Times.Once());
        }

        [Fact]
        public void Asignar_ConAsociacion_BorraAsociacion()
        {
            List<AsignacionCliente> asignaciones = new List<AsignacionCliente>();
            asignaciones.Add(new AsignacionCliente());

            clienteRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Cliente());
            patioRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Patio());
            asociacionClienteRepository.Setup(x => x.ObtenerAsociacion(It.IsAny<int>())).Returns(asignaciones);
            asociacionClienteRepository.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();
            asociacionClienteRepository.Setup(x => x.Add(It.IsAny<AsignacionCliente>())).Verifiable();

            AsignacionClienteDTO dto = new AsignacionClienteDTO();

            AsignacionClienteService asignacionCliente = new AsignacionClienteService(asociacionClienteRepository.Object, clienteRepository.Object, patioRepository.Object);
            asignacionCliente.Asignar(dto);

            asociacionClienteRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
            asociacionClienteRepository.Verify(x => x.Add(It.IsAny<AsignacionCliente>()), Times.Once());
        }
    }
}