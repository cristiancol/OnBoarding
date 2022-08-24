using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Repository.Repositories
{
    public class MarcaRepository : GenericRepository<Marca>, IMarcaRepository
    {
        public MarcaRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
