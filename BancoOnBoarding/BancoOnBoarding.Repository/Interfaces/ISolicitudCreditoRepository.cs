using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Repository.Interfaces
{
    public interface ISolicitudCreditoRepository : IGenericRepository<SolicitudCredito>
    {
        bool TieneCreditosDiferentePatio(int clienteId, int patioId);
        bool TieneCreditosHoy(int clienteId);
        bool VehiculoReservado(int vehiculoId);
    }
}
