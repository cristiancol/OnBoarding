using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Repository.Interfaces
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Cliente? ObtenerPorIdentificacion(string identificacion);
    }
}
