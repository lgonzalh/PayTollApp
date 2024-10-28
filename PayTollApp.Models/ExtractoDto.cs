namespace PayTollApp.Models
{
    public class ExtractoDto
    {
        public string ? NumeroTarjeta { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal TotalRecargas { get; set; }
        public decimal TotalPagos { get; set; }
        public decimal SaldoActual { get; set; }
        public List<MovimientoDto> ? Movimientos { get; set; }
    }

    public class MovimientoDto
    {
        public DateTime Fecha { get; set; }
        public string ? Descripcion { get; set; }
        public string ? Peaje { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
