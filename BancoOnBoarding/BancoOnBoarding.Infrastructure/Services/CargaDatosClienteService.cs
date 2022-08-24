using BancoOnBoarding.Domain.Interfaces;
using BancoOnBoarding.Entities.DTOs;
using BancoOnBoarding.Entities.ExtensionMethods;
using BancoOnBoarding.Infrastructure.Exceptions;
using BancoOnBoarding.Repository.Interfaces;
using CsvHelper;
using System.Globalization;

namespace BancoOnBoarding.Infrastructure.Services
{
    public class CargaDatosClienteService : ICargaDatosClienteService
    {
        private readonly IClienteRepository _repository;

        public CargaDatosClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public void Cargar()
        {
            IEnumerable<ClienteDTO> clientes;
            using (var reader = new StreamReader(@"Files\Cliente.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    clientes = csv.GetRecords<ClienteDTO>().ToList();
                }
            }

            var ClientesAgrupadosPorId = clientes.GroupBy(x => x.Id).Where(x => x.Count() > 1);

            if (ClientesAgrupadosPorId.Any())
            {
                throw new BancoOnBoardingException("Hay registros duplicados de cliente por ID");
            }

            var ClientesAgrupadosPorIdentificacion = clientes.GroupBy(x => x.Identificacion).Where(x => x.Count() > 1);

            if (ClientesAgrupadosPorIdentificacion.Any())
            {
                throw new BancoOnBoardingException("Hay registros duplicados de cliente por identificación");
            }

            foreach (var cliente in clientes)
            {
                bool ejecutivoExistente = _repository.Filter(x => x.Id == cliente.Id || x.Identificacion == cliente.Identificacion).Any();

                if (ejecutivoExistente)
                {
                    throw new BancoOnBoardingException($"El cliente con el id {cliente.Id} e identificación {cliente.Identificacion} ya existe.");
                }

                _repository.Add(cliente.GetEntity());
            }

            _repository.Save();
        }
    }
}
