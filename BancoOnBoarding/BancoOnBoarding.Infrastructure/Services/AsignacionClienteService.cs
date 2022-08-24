using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class AsignacionClienteService : IAsignacionClienteService
    {
        private readonly IAsignacionClienteRepository _asociacionClienteRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPatioRepository _patioRepository;

        public AsignacionClienteService(IAsignacionClienteRepository asociacionClienteRepository,
            IClienteRepository clienteRepository,
            IPatioRepository patioRepository)
        {
            _asociacionClienteRepository = asociacionClienteRepository;
            _clienteRepository = clienteRepository;
            _patioRepository = patioRepository;        
        }

        public void Asignar(AsignacionClienteDTO dto)
        {
            Cliente cliente = _clienteRepository.Get(dto.ClienteId);

            if (cliente == null)
            {
                throw new BancoOnBoardingException("El cliente no existe");
            }

            Patio patio = _patioRepository.Get(dto.PatioId);

            if (patio == null)
            {
                throw new BancoOnBoardingException("El patio no existe");
            }

            var asignaciones = _asociacionClienteRepository.ObtenerAsociacion(dto.ClienteId);

            if (asignaciones.Any())
            {
                asignaciones.ToList().ForEach(asignacion => _asociacionClienteRepository.Delete(asignacion.Id));
            }

            AsignacionCliente asignacion = dto.GetEntity();

            _asociacionClienteRepository.Add(asignacion);
            _asociacionClienteRepository.Save();
        }
    }
}
