using BancoOnBoarding.Entities.Entities;
using BancoOnBoarding.Entities.Enum;
using BancoOnBoarding.Repository.Interfaces;

namespace BancoOnBoarding.Repository.Repositories
{
    public class SolicitudCreditoRepository : GenericRepository<SolicitudCredito>, ISolicitudCreditoRepository
    {
        public SolicitudCreditoRepository(ApplicationContext context) : base(context)
        {
        }

        public bool TieneCreditosDiferentePatio(int clienteId, int patioId)
        {
            return Filter(c => c.ClienteId == clienteId && c.PatioId != patioId && c.Estado == EstadosSolicitudCredito.Registrada.ToString()).Any();
        }

        public bool TieneCreditosHoy(int clienteId)
        {
            return Filter(c => c.ClienteId == clienteId && 
            c.FechaElaboracion.Year == DateTime.Now.Year && 
            c.FechaElaboracion.Month == DateTime.Now.Month &&
            c.FechaElaboracion.Day == DateTime.Now.Day &&
            c.Estado == EstadosSolicitudCredito.Registrada.ToString()).Any();
        }

        public bool VehiculoReservado(int vehiculoId)
        {
            return Filter(c => c.VehiculoId == vehiculoId && c.Estado == EstadosSolicitudCredito.Registrada.ToString()).Any();
        }
    }
}
