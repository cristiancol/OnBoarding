namespace BancoOnBoarding.Entities.Entities
{
    public class AsignacionCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int PatioId { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
