using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Entities.Enum;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Infrastructure.Services;
using BancoOnBoarding.Repository.Interfaces;
using Moq;

namespace BancoOnBoarding.Test.Infrastructure
{
    public class SolicitudCreditoServiceTest
    {
        private Mock<ISolicitudCreditoRepository> _solicitudCreditoRepository;
        private Mock<IAsignacionClienteService> _asignacionClienteService;
        private Mock<IEjecutivoRepository> _ejecutivoRepository;
        private Mock<IVehiculoRepository> _vehiculoRepository;
        private SolicitudCreditoService _service;

        public SolicitudCreditoServiceTest()
        {
            _solicitudCreditoRepository = new Mock<ISolicitudCreditoRepository>();
            _asignacionClienteService = new Mock<IAsignacionClienteService>();
            _ejecutivoRepository = new Mock<IEjecutivoRepository>();
            _vehiculoRepository = new Mock<IVehiculoRepository>();

            _service = new SolicitudCreditoService(_solicitudCreditoRepository.Object,
                _ejecutivoRepository.Object,
                _asignacionClienteService.Object,
                _vehiculoRepository.Object);
        }

        [Fact]
        public void Solicitar_ConCreditosEnOtroPatio_LanzaExcepcion()
        {
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosDiferentePatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosHoy(
                It.IsAny<int>())).Returns(false);

            Assert.Throws<BancoOnBoardingException>(() => _service.Solicitar(new SolicitudCreditoDTO()));
        }

        [Fact]
        public void Solicitar_ConCreditosMismoDia_LanzaExcepcion()
        {
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosDiferentePatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosHoy(
                It.IsAny<int>())).Returns(true);

            Assert.Throws<BancoOnBoardingException>(() => _service.Solicitar(new SolicitudCreditoDTO()));
        }

        [Fact]
        public void Solicitar_EjecutivoNoEncontrado_LanzaExcepcion()
        {
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosDiferentePatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosHoy(
                It.IsAny<int>())).Returns(true);
            _ejecutivoRepository.Setup(x => x.PerteneceAlPatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            Assert.Throws<BancoOnBoardingException>(() => _service.Solicitar(new SolicitudCreditoDTO()));
        }

        [Fact]
        public void Solicitar_VehiculoNoExistente_LanzaExcepcion()
        {
            Vehiculo vehiculoNulo = null;

            _solicitudCreditoRepository.Setup(x => x.TieneCreditosDiferentePatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosHoy(
                It.IsAny<int>())).Returns(false);
            _ejecutivoRepository.Setup(x => x.PerteneceAlPatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _vehiculoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculoNulo);

            Assert.Throws<BancoOnBoardingException>(() => _service.Solicitar(new SolicitudCreditoDTO()));
        }

        [Fact]
        public void Solicitar_VehiculoReservado_LanzaExcepcion()
        {
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosDiferentePatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosHoy(
                It.IsAny<int>())).Returns(false);
            _ejecutivoRepository.Setup(x => x.PerteneceAlPatio(
                It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _vehiculoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vehiculo());
            _solicitudCreditoRepository.Setup(x => x.VehiculoReservado(It.IsAny<int>())).Returns(true);

            Assert.Throws<BancoOnBoardingException>(() => _service.Solicitar(new SolicitudCreditoDTO()));
        }

        [Fact]
        public void Solicitar_SinCreditos_CreaSolicitud()
        {
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosDiferentePatio(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _solicitudCreditoRepository.Setup(x => x.TieneCreditosHoy(It.IsAny<int>())).Returns(false);
            _ejecutivoRepository.Setup(x => x.PerteneceAlPatio(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _vehiculoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vehiculo());
            _solicitudCreditoRepository.Setup(x => x.VehiculoReservado(It.IsAny<int>())).Returns(false);
            _asignacionClienteService.Setup(x => x.Asignar(It.IsAny<AsignacionClienteDTO>())).Verifiable();
            _solicitudCreditoRepository.Setup(x => x.Add(It.IsAny<SolicitudCredito>())).Verifiable();

            _service.Solicitar(new SolicitudCreditoDTO());

            _asignacionClienteService.Verify(x => x.Asignar(It.IsAny<AsignacionClienteDTO>()), Times.Once());
            _solicitudCreditoRepository.Verify(x => x.Add(It.IsAny<SolicitudCredito>()), Times.Once());
        }

        [Fact]
        public void Despachar_SolicitudNoExiste_LanzaExcepcion()
        {
            SolicitudCredito solicitud = null;
            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);

            Assert.Throws<BancoOnBoardingException>(() => _service.Despachar(1));
        }

        [Fact]
        public void Despachar_SolicitudDespachada_LanzaExcepcion()
        {
            SolicitudCredito solicitud = new SolicitudCredito() 
            { 
                Estado = EstadosSolicitudCredito.Despachada.ToString() 
            };

            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);

            Assert.Throws<BancoOnBoardingException>(() => _service.Despachar(1));
        }

        [Fact]
        public void Despachar_SolicitudCancelada_LanzaExcepcion()
        {
            SolicitudCredito solicitud = new SolicitudCredito()
            {
                Estado = EstadosSolicitudCredito.Cancelada.ToString()
            };

            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);

            Assert.Throws<BancoOnBoardingException>(() => _service.Despachar(1));
        }

        [Fact]
        public void Despachar_SolicitudRegistrada_DespachaSolicitud()
        {
            SolicitudCredito solicitud = new SolicitudCredito()
            {
                Estado = EstadosSolicitudCredito.Registrada.ToString()
            };

            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);
            _solicitudCreditoRepository.Setup(x => x.Save()).Verifiable();

            _service.Despachar(1);

            _solicitudCreditoRepository.VerifyAll();
            Assert.True(solicitud.Estado == EstadosSolicitudCredito.Despachada.ToString());
        }

        [Fact]
        public void Cancelar_SolicitudNoExiste_LanzaExcepcion()
        {
            SolicitudCredito solicitud = null;
            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);

            Assert.Throws<BancoOnBoardingException>(() => _service.Cancelar(1));
        }

        [Fact]
        public void Cancelar_SolicitudDespachada_LanzaExcepcion()
        {
            SolicitudCredito solicitud = new SolicitudCredito()
            {
                Estado = EstadosSolicitudCredito.Despachada.ToString()
            };

            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);

            Assert.Throws<BancoOnBoardingException>(() => _service.Cancelar(1));
        }

        [Fact]
        public void Cancelar_SolicitudCancelada_LanzaExcepcion()
        {
            SolicitudCredito solicitud = new SolicitudCredito()
            {
                Estado = EstadosSolicitudCredito.Cancelada.ToString()
            };

            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);

            Assert.Throws<BancoOnBoardingException>(() => _service.Cancelar(1));
        }

        [Fact]
        public void Cancelar_SolicitudRegistrada_DespachaSolicitud()
        {
            SolicitudCredito solicitud = new SolicitudCredito()
            {
                Estado = EstadosSolicitudCredito.Registrada.ToString()
            };

            _solicitudCreditoRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(solicitud);
            _solicitudCreditoRepository.Setup(x => x.Save()).Verifiable();

            _service.Cancelar(1);

            _solicitudCreditoRepository.VerifyAll();
            Assert.True(solicitud.Estado == EstadosSolicitudCredito.Cancelada.ToString());
        }
    }
}
