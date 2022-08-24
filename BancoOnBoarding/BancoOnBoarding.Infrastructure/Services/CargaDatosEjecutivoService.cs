using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;
using CsvHelper;
using System.Globalization;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class CargaDatosEjecutivoService : ICargaDatosEjecutivoService
    {
        private readonly IEjecutivoRepository _repository;

        public CargaDatosEjecutivoService(IEjecutivoRepository repository)
        {
            _repository = repository;
        }

        public void Cargar()
        {
            IEnumerable<EjecutivoDTO> ejecutivos;
            using (var reader = new StreamReader(@"Files\Ejecutivo.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    ejecutivos = csv.GetRecords<EjecutivoDTO>().ToList();
                }
            }

            var ejecutivosAgrupadosPorId = ejecutivos.GroupBy(x => x.Id).Where(x => x.Count() > 1);

            if (ejecutivosAgrupadosPorId.Any())
            {
                throw new BancoOnBoardingException("Hay registros duplicados de ejecutivo por ID");
            }

            var ejecutivosAgrupadosPorIdentificacion = ejecutivos.GroupBy(x => x.Identificacion).Where(x => x.Count() > 1);

            if (ejecutivosAgrupadosPorIdentificacion.Any())
            {
                throw new BancoOnBoardingException("Hay registros duplicados de ejecutivo por identificación");
            }

            foreach (var ejecutivo in ejecutivos)
            {
                bool ejecutivoExistente = _repository.Filter(x => x.Id == ejecutivo.Id || x.Identificacion == ejecutivo.Identificacion).Any();

                if (ejecutivoExistente)
                {
                    throw new BancoOnBoardingException($"El ejecutivo con el id {ejecutivo.Id} e identificación {ejecutivo.Identificacion} ya existe.");
                }

                _repository.Add(ejecutivo.GetEntity());
            }

            _repository.Save();
        }
    }
}
