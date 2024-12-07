using Microsoft.AspNetCore.Mvc;

namespace CasoPratico2_G2.Pages.Models
{
    public class HistorialModel
    {
        public string JuegoId { get; set; }
        public string Jugador1 { get; set; }
        public string Jugador2 { get; set; }
        public string Ganador { get; set; }
        public TimeSpan Duracion { get; set; }
    }
}
