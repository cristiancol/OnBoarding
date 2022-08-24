using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Repository.Repositories
{
    public class AsignacionClienteRepository : GenericRepository<AsignacionCliente>, IAsignacionClienteRepository
    {
        public AsignacionClienteRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<AsignacionCliente> ObtenerAsociacion(int clienteId)
        {
            return Filter(x => x.ClienteId == clienteId);
        }
    }
}
