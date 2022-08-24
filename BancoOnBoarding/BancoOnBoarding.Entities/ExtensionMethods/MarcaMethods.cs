using AutoMapper;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Entities.ExtensionMethods
{
    public static class MarcaMethods
    {
        public static MarcaDTO GetDTO(this Marca Marca)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Marca, MarcaDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<MarcaDTO>(Marca);
        }

        public static Marca GetEntity(this MarcaDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MarcaDTO, Marca>());
            var mapper = config.CreateMapper();
            return mapper.Map<Marca>(dto);
        }
    }
}
