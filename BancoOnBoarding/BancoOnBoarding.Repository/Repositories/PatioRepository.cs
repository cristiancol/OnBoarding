using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Repository.Repositories
{
    public class PatioRepository : GenericRepository<Patio>, IPatioRepository
    {
        public PatioRepository(ApplicationContext context) : base(context)
        {
        }

        public bool ExistePuntoVenta(string numeroPuntoVenta)
        {
            return Filter(p => p.NumeroPuntoVenta == numeroPuntoVenta).Any();
        }
    }
}
