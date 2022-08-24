using BancoOnBoarding.Entities.DTOs;

namespace BancoOnBoarding.Domain.Interfaces
{
    public interface IVehiculoService
    {
        IEnumerable<VehiculoDTO> Obtener();
        VehiculoDTO Obtener(int id);
        void Crear(VehiculoDTO dto);
        void Actualizar(VehiculoDTO dto);
        void Borrar(int id);
    }
}
