using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Repository.Repositories
{
    public class EjecutivoRepository : GenericRepository<Ejecutivo>, IEjecutivoRepository
    {
        public EjecutivoRepository(ApplicationContext context) : base(context)
        {
        }

        public bool PerteneceAlPatio(int ejecutivoId, int patioId)
        {
            return Filter(e => e.Id == ejecutivoId && e.PatioId == patioId).Any();
        }
    }
}
