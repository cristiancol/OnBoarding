using BancoOnBoarding.Entities.DTOs;

namespace BancoOnBoarding.Domain.Interfaces
{
    public interface ISolicitudCreditoService
    {
        void Solicitar(SolicitudCreditoDTO dto);
        void Despachar(int id);
        void Cancelar(int id);
    }
}
