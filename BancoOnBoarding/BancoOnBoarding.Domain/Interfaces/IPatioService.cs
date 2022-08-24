using BancoOnBoarding.Entities.DTOs;

namespace BancoOnBoarding.Domain.Interfaces
{
    public interface IPatioService
    {
        IEnumerable<PatioDTO> Obtener();
        PatioDTO Obtener(int id);
        void Crear(PatioDTO dto);
        void Actualizar(PatioDTO dto);
        void Borrar(int id);
    }
}
