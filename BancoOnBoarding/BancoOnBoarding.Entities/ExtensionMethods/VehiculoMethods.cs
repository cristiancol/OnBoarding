using AutoMapper;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.Entities;

namespace BancoOnBoarding.Entities.ExtensionMethods
{
    public static class VehiculoMethods
    {
        public static VehiculoDTO GetDTO(this Vehiculo Vehiculo)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Vehiculo, VehiculoDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<VehiculoDTO>(Vehiculo);
        }

        public static Vehiculo GetEntity(this VehiculoDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<VehiculoDTO, Vehiculo>());
            var mapper = config.CreateMapper();
            return mapper.Map<Vehiculo>(dto);
        }
    }
}
