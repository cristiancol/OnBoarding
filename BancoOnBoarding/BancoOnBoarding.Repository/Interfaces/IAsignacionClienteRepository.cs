using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Repository.Interfaces
{
    public interface IAsignacionClienteRepository : IGenericRepository<AsignacionCliente>
    {
        public IEnumerable<AsignacionCliente> ObtenerAsociacion(int clienteId);
    }
}
