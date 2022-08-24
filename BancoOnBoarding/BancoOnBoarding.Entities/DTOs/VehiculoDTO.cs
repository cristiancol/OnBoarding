
namespace BancoOnBoarding.Entities.DTOs
{
    public class VehiculoDTO
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string NroChasis { get; set; }
        public string? Tipo { get; set; }
        public int Cilindraje { get; set; }
        public Decimal Avaluo { get; set; }
    }
}
