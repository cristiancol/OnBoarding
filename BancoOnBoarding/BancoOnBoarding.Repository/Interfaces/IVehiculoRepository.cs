using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Repository.Interfaces
{
    public interface IVehiculoRepository : IGenericRepository<Vehiculo>
    {
        Vehiculo ObtenerPorPlaca(string placa);
    }
}
