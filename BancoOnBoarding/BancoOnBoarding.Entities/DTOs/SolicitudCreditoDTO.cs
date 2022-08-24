namespace BancoOnBoarding.Entities.DTOs
{
    public class SolicitudCreditoDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int PatioId { get; set; }
        public int EjecutivoId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaElaboracion { set; get; }
        public DateTime Entrada { set; get; }
        public int MesesPlazo { get; set; }
        public int Cuotas { get; set; }
        public string? Observacion { set; get; }
        public string? Estado { set; get; }
    }
}
