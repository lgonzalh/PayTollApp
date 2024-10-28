namespace PayTollApp.Models
{
    public class TarjetaDto
    {
        public int Id { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? NumeroTarjeta { get; set; } // Aquí almacenamos el número enmascarado
        public int IdVehiculo { get; set; }
    }
}
