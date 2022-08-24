using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Entities.Enum;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class SolicitudCreditoService : ISolicitudCreditoService
    {
        private readonly ISolicitudCreditoRepository _repository;
        private readonly IEjecutivoRepository _ejecutivoRepository;
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IAsignacionClienteService _asociacionClienteService;
        public SolicitudCreditoService(ISolicitudCreditoRepository repository,
            IEjecutivoRepository ejecutivoRepository,
            IAsignacionClienteService asociacionClienteService,
            IVehiculoRepository vehiculoRepository)
        {
            _repository = repository;
            _ejecutivoRepository = ejecutivoRepository;
            _asociacionClienteService = asociacionClienteService;
            _vehiculoRepository = vehiculoRepository;
        }

        public void Cancelar(int id)
        {
            ActualizarEstado(id, EstadosSolicitudCredito.Cancelada);
        }

        public void Despachar(int id)
        {
            ActualizarEstado(id, EstadosSolicitudCredito.Despachada);
        }

        public void Solicitar(SolicitudCreditoDTO dto)
        {
            ValidarDatosSolicitud(dto);

            _asociacionClienteService.Asignar(new AsignacionClienteDTO {
                ClienteId = dto.ClienteId,
                PatioId = dto.PatioId,
                FechaAsignacion = DateTime.Now
            });

            SolicitudCredito solicitud = dto.GetEntity();

            solicitud.Estado = EstadosSolicitudCredito.Registrada.ToString();
            solicitud.FechaElaboracion = DateTime.Now;

            _repository.Add(solicitud);
            _repository.Save();
        }

        private void ValidarDatosSolicitud(SolicitudCreditoDTO dto) 
        {
            bool tieneCreditosHoy = _repository.TieneCreditosHoy(dto.ClienteId);

            if (tieneCreditosHoy)
            {
                throw new BancoOnBoardingException("El cliente ya solicitó un credito el día de hoy.");
            }

            bool tieneCreditoOtroPatio = _repository.TieneCreditosDiferentePatio(dto.ClienteId, dto.PatioId);

            if (tieneCreditoOtroPatio)
            {
                throw new BancoOnBoardingException("El cliente ya tiene un credito con otro patio.");
            }

            bool ejecutivoPerteneceAlPatio = _ejecutivoRepository.PerteneceAlPatio(dto.EjecutivoId, dto.PatioId);

            if (!ejecutivoPerteneceAlPatio)
            {
                throw new BancoOnBoardingException("El ejecutivo no se encontro para el patio indicado.");
            }

            Vehiculo vehiculo = _vehiculoRepository.Get(dto.VehiculoId);

            if (vehiculo == null)
            {
                throw new BancoOnBoardingException("El vehiculo indicado no existe.");
            }

            bool vehiculoReservado = _repository.VehiculoReservado(dto.VehiculoId);

            if (vehiculoReservado)
            {
                throw new BancoOnBoardingException("El vehiculo indicado se encuentra reservado.");
            }
        }

        private void ActualizarEstado(int id,  EstadosSolicitudCredito estado) 
        {
            SolicitudCredito solicitud = _repository.Get(id);

            if (solicitud == null)
            {
                throw new BancoOnBoardingException("La solicitud indicada no existe.");
            }

            if (solicitud.Estado != EstadosSolicitudCredito.Registrada.ToString())
            {
                throw new BancoOnBoardingException("No se puede actualizar la solicitud en este estado.");
            }

            solicitud.Estado = estado.ToString();
            _repository.Save();
        }
    }
}
