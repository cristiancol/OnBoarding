using AutoMapper;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Entities.ExtensionMethods
{
    public static class EjecutivoMethods
    {
        public static EjecutivoDTO GetDTO(this Ejecutivo ejecutivo)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Ejecutivo, EjecutivoDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<EjecutivoDTO>(ejecutivo);
        }

        public static Ejecutivo GetEntity(this EjecutivoDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EjecutivoDTO, Ejecutivo>());
            var mapper = config.CreateMapper();
            return mapper.Map<Ejecutivo>(dto);
        }
    }
}
