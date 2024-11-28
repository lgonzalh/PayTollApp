using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PayTollCardApi.Models;
using PayTollCardApi.DataAccess;

namespace PayTollCardApi.SharedServices
{
    public class TarjetaService(TarjetasDbContext context)
    {
        private readonly TarjetasDbContext _context = context;

        public async Task CrearTarjetaParaVehiculoAsync(int idUsuario, int idVehiculo)
        {
            var ultimaTarjeta = await _context.Tarjetas
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            int secuenciaCincoDigitos = 25630;
            int secuenciaTresDigitos = 29;
            int secuenciaDosDigitos = 78;

            if (ultimaTarjeta != null)
            {
                secuenciaCincoDigitos = int.Parse(ultimaTarjeta.NumeroTarjeta.Substring(6, 5)) + 2;
                secuenciaTresDigitos = int.Parse(ultimaTarjeta.NumeroTarjeta.Substring(11, 3)) + 3;
                secuenciaDosDigitos = int.Parse(ultimaTarjeta.NumeroTarjeta.Substring(14, 2));
            }

            if (secuenciaCincoDigitos >= 100000)
            {
                secuenciaCincoDigitos %= 100000;
                secuenciaTresDigitos++;
            }
            if (secuenciaTresDigitos >= 1000)
            {
                secuenciaTresDigitos %= 1000;
                secuenciaDosDigitos++;
            }
            if (secuenciaDosDigitos >= 100)
            {
                secuenciaDosDigitos %= 100;
            }

            string nuevoTarjetaNumero = "450691" +
                secuenciaCincoDigitos.ToString("D5") +
                secuenciaTresDigitos.ToString("D3") +
                secuenciaDosDigitos.ToString("D2");

            var nuevaTarjeta = new Tarjeta
            {
                IdUsuario = idUsuario,
                IdVehiculo = idVehiculo,
                Saldo = 0,
                FechaCreacion = DateTime.Now,
                NumeroTarjeta = nuevoTarjetaNumero
            };

            _context.Tarjetas.Add(nuevaTarjeta);
            await _context.SaveChangesAsync();
        }
    }
}
