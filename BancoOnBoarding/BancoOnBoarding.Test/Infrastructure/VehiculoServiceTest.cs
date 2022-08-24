using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Infrastructure.Services;
using BancoOnBoarding.Repository.Interfaces;
using Moq;

namespace BancoOnBoarding.Test.Infrastructure
{
    public class VehiculoServiceTest
    {
        private Mock<IVehiculoRepository> _repository;
        private Mock<ISolicitudCreditoRepository> _solicitudCreditoRepository;
        private VehiculoService _service;

        public VehiculoServiceTest()
        {
            _repository = new Mock<IVehiculoRepository>();
            _solicitudCreditoRepository = new Mock<ISolicitudCreditoRepository>();
            _service = new VehiculoService(_repository.Object, _solicitudCreditoRepository.Object);
        }

        [Fact]
        public void Crear_ConIdExistente_LanzaExcepcion()
        {
            Vehiculo vehiculo = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vehiculo());
            _repository.Setup(x => x.ObtenerPorPlaca(It.IsAny<string>())).Returns(vehiculo);

            Assert.Throws<BancoOnBoardingException>(() => _service.Crear(new VehiculoDTO()));
        }

        [Fact]
        public void Crear_ConPlacaExistente_LanzaExcepcion()
        {
            Vehiculo vehiculo = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculo);
            _repository.Setup(x => x.ObtenerPorPlaca(It.IsAny<string>())).Returns(new Vehiculo());

            Assert.Throws<BancoOnBoardingException>(() => _service.Crear(new VehiculoDTO()));
        }

        [Fact]
        public void Crear_ConDatosValidos_CreaVehiculo()
        {
            Vehiculo vehiculo = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculo);
            _repository.Setup(x => x.ObtenerPorPlaca(It.IsAny<string>())).Returns(vehiculo);
            _repository.Setup(x => x.Add(It.IsAny<Vehiculo>())).Verifiable();
            _repository.Setup(x => x.Save()).Verifiable();

            _service.Crear(new VehiculoDTO());
            _repository.VerifyAll();
        }

        [Fact]
        public void Obtener_VehiculoNoExistente_RetornaNulo()
        {
            Vehiculo vehiculo = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculo);

            Assert.Null(_service.Obtener(1));
        }

        [Fact]
        public void Obtener_VehiculoExistente_RetornaVehiculo()
        {
            Vehiculo vehiculo = new Vehiculo();
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculo);

            Assert.NotNull(_service.Obtener(1));
        }

        [Fact]
        public void Obtener_SinVehiculos_RetornaListaVacia()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            _repository.Setup(x => x.Get()).Returns(vehiculos);

            Assert.False(_service.Obtener().Any());
        }

        [Fact]
        public void Obtener_VehiculosExistentes_RetornaLista()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            vehiculos.Add(new Vehiculo());
            _repository.Setup(x => x.Get()).Returns(vehiculos);

            Assert.True(_service.Obtener().Any());
        }

        [Fact]
        public void Actualizar_VehiculoNoExistente_LanzaExcepcion()
        {
            Vehiculo vehiculo = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculo);
            _repository.Setup(x => x.ObtenerPorPlaca(It.IsAny<string>())).Returns(vehiculo);

            Assert.Throws<BancoOnBoardingException>(() => _service.Actualizar(new VehiculoDTO()));
        }

        [Fact]
        public void Actualizar_ConPlacaUsada_LanzaExcepcion() 
        {
            Vehiculo vehiculoNulo = null;

            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Placa = "123";

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculo);
            _repository.Setup(x => x.ObtenerPorPlaca(It.IsAny<string>())).Returns(new Vehiculo());

            Assert.Throws<BancoOnBoardingException>(() => _service.Actualizar(new VehiculoDTO() { Placa = "1234"}));
        }

        [Fact]
        public void Actualizar_ConDatosValidos_ActualizaVehiculo() 
        {
            Vehiculo vehiculoNulo = null;

            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Placa = "123";

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculo);
            _repository.Setup(x => x.ObtenerPorPlaca(It.IsAny<string>())).Returns(vehiculoNulo);
            _repository.Setup(x => x.Save()).Verifiable();

            _service.Actualizar(new VehiculoDTO() { Placa = "1234" });
            _repository.VerifyAll();
        }

        [Fact]
        public void Borrar_VehiculoNoExistente_LanzaExcepcion()
        {
            Vehiculo vehiculoNulo = null;
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(vehiculoNulo);

            Assert.Throws<BancoOnBoardingException>(() => _service.Borrar(1));
        }

        [Fact]
        public void Borrar_ConSolicitudesAsociadas_LanzaExcepcion()
        {
            List<SolicitudCredito> solicitudes = new List<SolicitudCredito>();
            solicitudes.Add(new SolicitudCredito());

            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vehiculo());
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(solicitudes);

            Assert.Throws<BancoOnBoardingException>(() => _service.Borrar(1));
        }

        [Fact]
        public void Borrar_SinRelaciones_BorraVehiculo()
        {
            _repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Vehiculo());
            _solicitudCreditoRepository.Setup(x => x.Filter(It.IsAny<Func<SolicitudCredito, bool>>())).Returns(new List<SolicitudCredito>());
            _repository.Setup(x => x.Save()).Verifiable();

            _service.Borrar(1);
            _repository.VerifyAll();
        }
    }
}
