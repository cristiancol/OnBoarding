using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly ISolicitudCreditoRepository _solicitudCreditoRepository;
        private readonly IAsignacionClienteRepository _asignacionClienteRepository;

        public ClienteService(IClienteRepository repository, 
            ISolicitudCreditoRepository solicitudCreditoRepository, 
            IAsignacionClienteRepository asignacionClienteRepository)
        {
            _repository = repository;
            _solicitudCreditoRepository = solicitudCreditoRepository;
            _asignacionClienteRepository = asignacionClienteRepository;
        }

        public void Actualizar(ClienteDTO dto)
        {
            Cliente clienteExistente = _repository.Get(dto.Id);

            if (clienteExistente == null)
            {
                throw new BancoOnBoardingException("El cliente con el Id indicado no existe");
            }

            if (clienteExistente.Identificacion != dto.Identificacion &&
                _repository.ObtenerPorIdentificacion(dto.Identificacion) != null)
            {
                throw new BancoOnBoardingException("Ya hay un cliente con la identificación que esta intentando actualizar");
            }

            clienteExistente.Identificacion = dto.Identificacion;
            clienteExistente.Nombres = dto.Nombres;
            clienteExistente.Apellidos = dto.Apellidos;
            clienteExistente.Direccion = dto.Direccion;
            clienteExistente.Edad = dto.Edad;
            clienteExistente.EstadoCivil = dto.EstadoCivil;
            clienteExistente.FechaNacimiento = dto.FechaNacimiento;
            clienteExistente.IdentificacionConyuge = dto.IdentificacionConyuge;
            clienteExistente.NombreConyuge = dto.NombreConyuge;

            _repository.Save();
        }

        public void Borrar(int id)
        {
            Cliente cliente = _repository.Get(id);

            if (cliente == null)
            {
                throw new BancoOnBoardingException("El Cliente con el Id indicado no existe");
            }

            var asignaciones = _asignacionClienteRepository.ObtenerAsociacion(id);

            if (_asignacionClienteRepository.ObtenerAsociacion(id).Any()
                || _solicitudCreditoRepository.Filter(x => x.ClienteId == id).Any())
            {
                throw new BancoOnBoardingException("No se puede borrar el registro por que tiene aregistros asociados");
            }

            _repository.Delete(id);
            _repository.Save();
        }

        public void Crear(ClienteDTO dto)
        {
            Cliente? clienteExistente = _repository.Get(dto.Id);

            if (clienteExistente != null)
            {
                throw new BancoOnBoardingException("Ya existe un cliente con el Id indicado");
            }

            clienteExistente = _repository.ObtenerPorIdentificacion(dto.Identificacion);

            if (clienteExistente != null)
            {
                throw new BancoOnBoardingException("Ya existe un cliente con el número de identificación indicado.");
            }

            Cliente cliente = dto.GetEntity();

            _repository.Add(cliente);
            _repository.Save();
        }

        public IEnumerable<ClienteDTO> Obtener()
        {
            List<ClienteDTO> clientesDTO = new List<ClienteDTO>();
            IEnumerable<Cliente> clientes = _repository.Get();

            clientes.ToList().ForEach(vehiculo =>
                clientesDTO.Add(vehiculo.GetDTO())
            );

            return clientesDTO;
        }

        public ClienteDTO Obtener(int id)
        {
            return _repository.Get(id).GetDTO();
        }
    }
}
