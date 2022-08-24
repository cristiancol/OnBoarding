using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Repository.Interfaces
{
    public interface IPatioRepository : IGenericRepository<Patio>
    {
        bool ExistePuntoVenta(string numeroPuntoVenta);
    }
}
