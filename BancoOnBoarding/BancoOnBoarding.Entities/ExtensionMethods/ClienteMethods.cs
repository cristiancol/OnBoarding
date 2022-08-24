using AutoMapper;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Entities.ExtensionMethods
{
    public static class ClienteMethods
    {
        public static ClienteDTO GetDTO(this Cliente Cliente)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cliente, ClienteDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<ClienteDTO>(Cliente);
        }

        public static Cliente GetEntity(this ClienteDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ClienteDTO, Cliente>());
            var mapper = config.CreateMapper();
            return mapper.Map<Cliente>(dto);
        }
    }
}
