using BancoOnBoarding.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BancoOnBoarding.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<AsignacionCliente>? AsignacionCliente { get; set; }
        public DbSet<SolicitudCredito>? SolicitudCredito { get; set; }
        public DbSet<Cliente>? Cliente { get; set; }
        public DbSet<Patio>? Patio { get; set; }
        public DbSet<Ejecutivo>? Ejecutivo { get; set; }
        public DbSet<Vehiculo>? Vehiculo { get; set; }
        public DbSet<Marca>? Marca { get; set; }
    }
}