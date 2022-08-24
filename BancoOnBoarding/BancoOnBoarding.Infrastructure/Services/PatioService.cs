using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class PatioService : IPatioService
    {
        private readonly IPatioRepository _repository;
        private readonly IEjecutivoRepository _ejecutivoRepository;
        private readonly IAsignacionClienteRepository _asignacionClienteRepository;
        private readonly ISolicitudCreditoRepository _solicitudCreditoRepository;
        public PatioService(IPatioRepository repository, 
            IEjecutivoRepository ejecutivoRepository, 
            IAsignacionClienteRepository asignacionClienteRepository, 
            ISolicitudCreditoRepository solicitudCreditoRepository)
        {
            _repository = repository;
            _ejecutivoRepository = ejecutivoRepository;
            _asignacionClienteRepository = asignacionClienteRepository;
            _solicitudCreditoRepository = solicitudCreditoRepository;
        }

        public void Actualizar(PatioDTO dto)
        {
            Patio patioExistente = _repository.Get(dto.Id);

            if (patioExistente == null)
            {
                throw new BancoOnBoardingException("El patio con el id indicado no existe.");
            }

            if (dto.NumeroPuntoVenta != patioExistente.NumeroPuntoVenta
                && _repository.ExistePuntoVenta(dto.NumeroPuntoVenta))
            {
                throw new BancoOnBoardingException("El número de punto de venta ya esta siendo usado por otro patio.");
            }

            patioExistente.Nombre = dto.Nombre;
            patioExistente.Direccion = dto.Direccion;
            patioExistente.Telefono = dto.Telefono;
            patioExistente.NumeroPuntoVenta = dto.NumeroPuntoVenta;

            _repository.Save();
        }

        public void Borrar(int id)
        {
            if (_ejecutivoRepository.Filter(e => e.PatioId == id).Any() ||
                _asignacionClienteRepository.Filter(a => a.PatioId == id).Any() ||
                _solicitudCreditoRepository.Filter(s => s.PatioId == id).Any())
            {
                throw new BancoOnBoardingException("El patio no se puede borrar ya que tiene registros relacionados.");
            }

            _repository.Delete(id);
            _repository.Save();
        }

        public void Crear(PatioDTO dto)
        {
            Patio patioExistente = _repository.Get(dto.Id);

            if (patioExistente != null)
            {
                throw new BancoOnBoardingException("Ya existe un patio con el Id indicado.");
            }

            bool existePuntoVenta = _repository.ExistePuntoVenta(dto.NumeroPuntoVenta);

            if (existePuntoVenta)
            {
                throw new BancoOnBoardingException("Ya existe un patio con el número de punto de venta indicado.");
            }

            Patio patio = dto.GetEntity();

            _repository.Add(patio);
            _repository.Save();
        }

        public PatioDTO Obtener(int id)
        {
            return _repository.Get(id).GetDTO();
        }

        public IEnumerable<PatioDTO> Obtener()
        {
            List<PatioDTO> patiosDTO = new List<PatioDTO>(); 
            IEnumerable<Patio> patios = _repository.Get();

            patios.ToList().ForEach(patio =>
                patiosDTO.Add(patio.GetDTO())
            );

            return patiosDTO;
        }
    }
}
