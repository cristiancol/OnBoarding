using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Infrastructure.Services;
using BancoOnBoarding.Repository.Interfaces;
using Moq;

namespace BancoOnBoarding.Test.Infrastructure
{
    public class ClienteServiceTest
    {
        private Mock<IClienteRepository> _repository;
        private Mock<ISolicitudCreditoRepository> _solicitudCreditoRepository;
        private Mock<IAsignacionClienteRepository> _asignacionClienteRepository;

        public ClienteServiceTest()
        {
            _repository = new Mock<IClienteRepository>();
            _asignacionClienteRepository = new Mock<IAsignacionClienteRepository>();
            _solicitudCreditoRepository = new Mock<ISolicitudCreditoRepository>();
        }

        [Fact]
        public void Crear_ConIdentificacionExistente_LanzaExcepcion() 
        {
            Cliente cliente = null;
            _repository.Setup(x => x.ObtenerPorIdentificacion(It.IsAny<string>())).Returns(new Cliente());
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(cliente);

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Crear(new ClienteDTO()));
        }

        [Fact]
        public void Crear_ConIdExistente_LanzaExcepcion()
        {
            Cliente cliente = null;
            _repository.Setup(x => x.ObtenerPorIdentificacion(It.IsAny<string>())).Returns(cliente);
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Cliente());

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Crear(new ClienteDTO()));
        }

        [Fact]
        public void Crear_ConDatosValidos_CreaCliente()
        {
            Cliente cliente = null;
            _repository.Setup(x => x.ObtenerPorIdentificacion(It.IsAny<string>())).Returns(cliente);
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(cliente);
            _repository.Setup(x => x.Add(It.IsAny<Cliente>())).Verifiable();

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            service.Crear(new ClienteDTO());
            _repository.VerifyAll();
        }

        [Fact]
        public void Obtener_ClienteNoExistente_RetornaNulo()
        {
            Cliente cliente = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(cliente);

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.Null(service.Obtener(1));
        }

        [Fact]
        public void Obtener_ClienteExistente_RetornaCliente()
        {
            Cliente cliente = new Cliente();
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(cliente);

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.NotNull(service.Obtener(1));
        }

        [Fact]
        public void Obtener_SinClientes_RetornaListaVacia()
        {
            List<Cliente> clientes = new List<Cliente>();
            _repository.Setup(x => x.Get()).Returns(clientes);

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.False(service.Obtener().Any());
        }

        [Fact]
        public void Obtener_ClientesExistentes_RetornaListaConClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(new Cliente());
            _repository.Setup(x => x.Get()).Returns(clientes);

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.True(service.Obtener().Any());
        }

        [Fact]
        public void Borrar_RegistroNoExistente_LanzaExcepcion()
        {
            Cliente cliente = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(cliente);

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Borrar(1));
        }

        [Fact]
        public void Borrar_ConSolicitudesRelacionadas_LanzaExcepcion()
        {
            List<SolicitudCredito> solicitudes = new List<SolicitudCredito>();
            solicitudes.Add(new SolicitudCredito());

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Cliente());
            _asignacionClienteRepository.Setup(x => x.ObtenerAsociacion(It.IsAny<int>())).Returns(new List<AsignacionCliente>());
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(solicitudes);

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Borrar(1));
        }

        [Fact]
        public void Borrar_ConAsignacionesRelacionadas_LanzaExcepcion()
        {
            List<AsignacionCliente> asignaciones = new List<AsignacionCliente>();
            asignaciones.Add(new AsignacionCliente());

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Cliente());
            _asignacionClienteRepository.Setup(x => x.ObtenerAsociacion(It.IsAny<int>())).Returns(asignaciones);
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(new List<SolicitudCredito>());

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Borrar(1));
        }

        [Fact]
        public void Borrar_SinRelaciones_BorraVehiculo()
        {
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Cliente());
            _asignacionClienteRepository.Setup(x => x.ObtenerAsociacion(It.IsAny<int>())).Returns(new List<AsignacionCliente>());
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(new List<SolicitudCredito>());
            _repository.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            service.Borrar(1);
            _repository.VerifyAll();
        }

        [Fact]
        public void Actualizar_ConIdentificacionExistente_LanzaExcepcion()
        {
            _repository.Setup(x => x.ObtenerPorIdentificacion(It.IsAny<string>())).Returns(new Cliente());

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Actualizar(new ClienteDTO()));
        }

        [Fact]
        public void Actualizar_ConDatosValidos_ActualizaCliente()
        {
            Cliente cliente = new Cliente();
            cliente.Identificacion = "123";
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(cliente);
            _repository.Setup(x => x.Save()).Verifiable();

            ClienteService service = new ClienteService(_repository.Object,
                _solicitudCreditoRepository.Object,
                _asignacionClienteRepository.Object);

            service.Actualizar(new ClienteDTO() { Identificacion = "123"});
            _repository.VerifyAll();
        }
    }
}
