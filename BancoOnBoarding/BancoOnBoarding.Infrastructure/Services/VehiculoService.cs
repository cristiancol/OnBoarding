using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _repository;
        private readonly ISolicitudCreditoRepository _solicitudCreditoRepository;

        public VehiculoService(IVehiculoRepository repository, 
            ISolicitudCreditoRepository solicitudCreditoRepository)
        {
            _repository = repository;
            _solicitudCreditoRepository = solicitudCreditoRepository;
        }

        public void Actualizar(VehiculoDTO dto)
        {
            Vehiculo vehiculoExistente = _repository.Get(dto.Id);

            if (vehiculoExistente == null)
            {
                throw new BancoOnBoardingException("El vehiculo con el Id ingresado no existe.");
            }

            if (vehiculoExistente.Placa != dto.Placa
                && _repository.ObtenerPorPlaca(dto.Placa) != null)
            {
                throw new BancoOnBoardingException("La placa que esta intentando actualizar ya esta siendo usada por otro vehiculo.");
            }

            vehiculoExistente.Placa = dto.Placa;
            vehiculoExistente.MarcaId = dto.MarcaId;
            vehiculoExistente.Modelo = dto.Modelo;
            vehiculoExistente.NroChasis = dto.NroChasis;
            vehiculoExistente.Tipo = dto.Tipo;
            vehiculoExistente.Cilindraje = dto.Cilindraje;
            vehiculoExistente.Avaluo = dto.Avaluo;

            _repository.Save();
        }

        public void Borrar(int id)
        {
            Vehiculo vehiculoExistente = _repository.Get(id);

            if (vehiculoExistente == null)
            {
                throw new BancoOnBoardingException("El vehiculo con el Id ingresado no existe.");
            }

            bool tieneSolicitudesAsociadas = _solicitudCreditoRepository.Filter(x => x.VehiculoId == id).Any();

            if (tieneSolicitudesAsociadas)
            {
                throw new BancoOnBoardingException("No se puede borrar el registro ya que tiene solicitudes asociadas");
            }

            _repository.Delete(id);
            _repository.Save();
        }

        public void Crear(VehiculoDTO dto)
        {
            Vehiculo vehiculoExistente = _repository.Get(dto.Id);

            if (vehiculoExistente != null)
            {
                throw new BancoOnBoardingException("El vehiculo con el Id ingresado ya existe.");
            }

            vehiculoExistente = _repository.ObtenerPorPlaca(dto.Placa);

            if (vehiculoExistente != null)
            {
                throw new BancoOnBoardingException("Ya existe un vehiculo con la placa ingresada.");
            }

            Vehiculo vehiculo = dto.GetEntity();

            _repository.Add(vehiculo);
            _repository.Save();
        }

        public IEnumerable<VehiculoDTO> Obtener()
        {
            List<VehiculoDTO> vehiculosDTO = new List<VehiculoDTO>();
            IEnumerable<Vehiculo> vehiculos = _repository.Get();

            vehiculos.ToList().ForEach(vehiculo =>
                vehiculosDTO.Add(vehiculo.GetDTO())
            );

            return vehiculosDTO;
        }

        public VehiculoDTO Obtener(int id)
        {
            return _repository.Get(id).GetDTO();
        }
    }
}
