using AutoMapper;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Entities.ExtensionMethods
{
    public static class SolicitudCreditoMethods
    {
        public static SolicitudCreditoDTO GetDTO(this SolicitudCredito SolicitudCredito)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SolicitudCredito, SolicitudCreditoDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<SolicitudCreditoDTO>(SolicitudCredito);
        }

        public static SolicitudCredito GetEntity(this SolicitudCreditoDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SolicitudCreditoDTO, SolicitudCredito>());
            var mapper = config.CreateMapper();
            return mapper.Map<SolicitudCredito>(dto);
        }
    }
}
