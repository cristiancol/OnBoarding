namespace BancoOnBoarding.Entities.Entities
{
    public class Ejecutivo
    {
        public int Id { get; set; }
        public int PatioId { get; set; }
        public string? Identificacion { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public int Edad { get; set; }
    }
}
