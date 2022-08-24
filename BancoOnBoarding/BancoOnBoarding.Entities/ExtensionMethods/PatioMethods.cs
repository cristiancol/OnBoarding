using AutoMapper;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Entities.ExtensionMethods
{
    public static class PatioMethods
    {
        public static PatioDTO GetDTO(this Patio Patio)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Patio, PatioDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<PatioDTO>(Patio);
        }

        public static Patio GetEntity(this PatioDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PatioDTO, Patio>());
            var mapper = config.CreateMapper();
            return mapper.Map<Patio>(dto);
        }
    }
}
