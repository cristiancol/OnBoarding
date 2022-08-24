using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;
using CsvHelper;
using System.Globalization;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class CargaDatosMarcaService : ICargaDatosMarcaService
    {
        private readonly IMarcaRepository _repository;

        public CargaDatosMarcaService(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public void Cargar()
        {
            IEnumerable<MarcaDTO> Marcas;
            using (var reader = new StreamReader(@"Files\Marcas.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    Marcas = csv.GetRecords<MarcaDTO>().ToList();
                }
            }

            var MarcasAgrupadosPorId = Marcas.GroupBy(x => x.Id).Where(x => x.Count() > 1);

            if (MarcasAgrupadosPorId.Any())
            {
                throw new BancoOnBoardingException("Hay registros duplicados de Marca por ID");
            }

            var MarcasAgrupadosPorIdentificacion = Marcas.GroupBy(x => x.Nombre).Where(x => x.Count() > 1);

            if (MarcasAgrupadosPorIdentificacion.Any())
            {
                throw new BancoOnBoardingException("Hay registros duplicados de Marca por nombre.");
            }

            foreach (var Marca in Marcas)
            {
                bool ejecutivoExistente = _repository.Filter(x => x.Id == Marca.Id || x.Nombre == Marca.Nombre).Any();

                if (ejecutivoExistente)
                {
                    throw new BancoOnBoardingException($"La Marca con el id {Marca.Id} y nombre {Marca.Nombre} ya existe.");
                }

                _repository.Add(Marca.GetEntity());
            }

            _repository.Save();
        }
    }
}
