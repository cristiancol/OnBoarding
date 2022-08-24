using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Repository.Repositories
{
    public class VehiculoRepository : GenericRepository<Vehiculo>, IVehiculoRepository
    {
        public VehiculoRepository(ApplicationContext context) : base(context)
        {
        }

        public Vehiculo ObtenerPorPlaca(string placa)
        {
            return Filter(x => x.Placa == placa).FirstOrDefault();
        }
    }
}
