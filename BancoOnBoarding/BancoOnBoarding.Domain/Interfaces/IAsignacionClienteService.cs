using BancoOnBoarding.Entities.DTOs;

namespace BancoOnBoarding.Domain.Interfaces
{
    public interface IAsignacionClienteService
    {
        public void Asignar(AsignacionClienteDTO dto);
    }
}
