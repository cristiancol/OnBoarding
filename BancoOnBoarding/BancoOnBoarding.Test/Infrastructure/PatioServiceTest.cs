using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Infrastructure.Services;
using BancoOnBoarding.Repository.Interfaces;
using Moq;

namespace BancoOnBoarding.Test.Infrastructure
{
    public class PatioServiceTest
    {
        private Mock<IPatioRepository> _repository;
        private Mock<IEjecutivoRepository> _ejecutivoRepository;
        private Mock<IAsignacionClienteRepository> _asignacionClienteRepository;
        private Mock<ISolicitudCreditoRepository> _solicitudCreditoRepository;

        public PatioServiceTest()
        {
            _repository = new Mock<IPatioRepository>();
            _ejecutivoRepository = new Mock<IEjecutivoRepository>();
            _asignacionClienteRepository = new Mock<IAsignacionClienteRepository>();
            _solicitudCreditoRepository = new Mock<ISolicitudCreditoRepository>();
        }

        [Fact]
        public void Crear_ConIdExistente_LanzaExcepcion()
        {
            _repository.Setup(x => x.Get(
                It.IsAny<int>())).Returns(new Patio());

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Crear(new PatioDTO()));
        }

        [Fact]
        public void Crear_ConNumeroPuntoVentaExistente_LanzaExcepcion()
        {
            Patio patio = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(true);

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Crear(new PatioDTO()));
        }

        [Fact]
        public void Crear_ConDatosValidos_CreaPatio()
        {
            Patio patio = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(false);
            _repository.Setup(x => x.Add(It.IsAny<Patio>())).Verifiable();

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            service.Crear(new PatioDTO());

            _repository.Verify(x => x.Add(It.IsAny<Patio>()), Times.Once());
        }

        [Fact]
        public void Borrar_ConEjecutivoRelacionado_LanzaExcepcion()
        {
            Patio patio = null;
            List<Ejecutivo> ejecutivos = new List<Ejecutivo>();
            ejecutivos.Add(new Ejecutivo());           

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(false);
            _ejecutivoRepository.Setup(x => x.Filter(It.IsAny<Func<Ejecutivo,bool>>())).Returns(ejecutivos);
            _asignacionClienteRepository.Setup(x => x.Filter(It.IsAny<Func<AsignacionCliente, bool>>())).Returns(new List<AsignacionCliente>());
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(new List<SolicitudCredito>());

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Borrar(It.IsAny<int>()));
        }

        [Fact]
        public void Borrar_ConAsignacionesRelacionadas_LanzaExcepcion()
        {
            Patio patio = null;

            List<AsignacionCliente> asignaciones = new List<AsignacionCliente>();
            asignaciones.Add(new AsignacionCliente());

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(false);
            _ejecutivoRepository.Setup(x => x.Filter(It.IsAny<Func<Ejecutivo, bool>>())).Returns(new List<Ejecutivo>());
            _asignacionClienteRepository.Setup(x => x.Filter(It.IsAny<Func<AsignacionCliente, bool>>())).Returns(asignaciones);
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(new List<SolicitudCredito>());

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Borrar(It.IsAny<int>()));
        }

        [Fact]
        public void Borrar_ConSolicitudesRelacionadas_LanzaExcepcion()
        {
            Patio patio = null;

            List<SolicitudCredito> solicitudes = new List<SolicitudCredito>();
            solicitudes.Add(new SolicitudCredito());

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(false);
            _ejecutivoRepository.Setup(x => x.Filter(It.IsAny<Func<Ejecutivo, bool>>())).Returns(new List<Ejecutivo>());
            _asignacionClienteRepository.Setup(x => x.Filter(It.IsAny<Func<AsignacionCliente, bool>>())).Returns(new List<AsignacionCliente>());
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(solicitudes);

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Borrar(It.IsAny<int>()));
        }

        [Fact]
        public void Borrar_SinRegistrosRelacionados_BorraPatio()
        {
            Patio patio = null;

            List<SolicitudCredito> solicitudes = new List<SolicitudCredito>();
            solicitudes.Add(new SolicitudCredito());

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(false);
            _ejecutivoRepository.Setup(x => x.Filter(It.IsAny<Func<Ejecutivo, bool>>())).Returns(new List<Ejecutivo>());
            _asignacionClienteRepository.Setup(x => x.Filter(It.IsAny<Func<AsignacionCliente, bool>>())).Returns(new List<AsignacionCliente>());
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(new List<SolicitudCredito>());
            _repository.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            service.Borrar(It.IsAny<int>());
            _repository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void Actualizar_PatioNoExiste_LanzaExcepcion()
        {
            Patio patio = null;

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Actualizar(new PatioDTO()));
        }

        [Fact]
        public void Actualizar_NumeroPuntoVentaExistente_LanzaExcepcion()
        {
            Patio patio = new Patio() { NumeroPuntoVenta = "1" };

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(true);

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Throws<BancoOnBoardingException>(() => service.Actualizar(new PatioDTO { NumeroPuntoVenta = "2" }));
        }

        [Fact]
        public void Actualizar_NumeroPuntoVentaNoExiste_ActualizaCorrectamente()
        {
            Patio patio = new Patio() { NumeroPuntoVenta = "1" };

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);
            _repository.Setup(x => x.ExistePuntoVenta(It.IsAny<string>())).Returns(false);
            _repository.Setup(x => x.Save()).Verifiable();

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            service.Actualizar(new PatioDTO { NumeroPuntoVenta = "2" });
            _repository.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public void Obtener_PatioNoExistente_RetornaNull()
        {
            Patio patio = null;

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.Null(service.Obtener(1));
        }

        [Fact]
        public void Obtener_PatioExistente_RetornaPatio()
        {
            Patio patio = new Patio();

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(patio);

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.NotNull(service.Obtener(1));
        }

        [Fact]
        public void Obtener_NoHayPatios_RetornaListaVacia()
        {
            _repository.Setup(x => x.Get()).Returns(new List<Patio>());

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.False(service.Obtener().Any());
        }

        [Fact]
        public void Obtener_HayPatios_RetornaListaConPatios()
        {
            List<Patio> patios = new List<Patio>();
            patios.Add(new Patio());
            _repository.Setup(x => x.Get()).Returns(patios);

            PatioService service = new PatioService(_repository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteRepository.Object,
                _solicitudCreditoRepository.Object);

            Assert.True(service.Obtener().Any());
        }
    }
}
