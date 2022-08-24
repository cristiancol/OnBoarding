using BancoOnBoarding.Entities.DTOs;

namespace BancoOnBoarding.Domain.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> Obtener();
        ClienteDTO Obtener(int id);
        void Crear(ClienteDTO dto);
        void Actualizar(ClienteDTO dto);
        void Borrar(int id);
    }
}
