using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Repository.Interfaces
{
    public interface IEjecutivoRepository : IGenericRepository<Ejecutivo>
    {
        bool PerteneceAlPatio(int ejecutivoId, int patioId);
    }
}
