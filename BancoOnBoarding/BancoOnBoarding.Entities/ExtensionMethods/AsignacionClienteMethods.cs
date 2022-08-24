using AutoMapper;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Entities.ExtensionMethods
{
    public static class AsignacionClienteMethods
    {
        public static AsignacionClienteDTO GetDTO(this AsignacionCliente AsignacionCliente)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AsignacionCliente, AsignacionClienteDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<AsignacionClienteDTO>(AsignacionCliente);
        }

        public static AsignacionCliente GetEntity(this AsignacionClienteDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AsignacionClienteDTO, AsignacionCliente>());
            var mapper = config.CreateMapper();
            return mapper.Map<AsignacionCliente>(dto);
        }
    }
}
