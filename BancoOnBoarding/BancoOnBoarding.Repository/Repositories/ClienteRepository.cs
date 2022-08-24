using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Repository.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationContext context) : base(context)
        {
        }

        public Cliente? ObtenerPorIdentificacion(string identificacion)
        {
            return Filter(c => c.Identificacion == identificacion).FirstOrDefault();
        }
    }
}
