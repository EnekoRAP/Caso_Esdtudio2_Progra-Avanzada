using CasoPratico2_G2.Pages.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasoPratico2_G2.Pages.Controller
{
    public class HistorialController : ControllerBase
    {
        private readonly List<HistorialModel> historial;

        public HistorialController()
        {
            historial = new List<HistorialModel>();
        }

        public void AgregarRegistro(string juegoId, string jugador1, string jugador2, string ganador, TimeSpan duracion)
        {
            var registro = new HistorialModel
            {
                JuegoId = juegoId,
                Jugador1 = jugador1,
                Jugador2 = jugador2,
                Ganador = ganador,
                Duracion = duracion
            };

            historial.Add(registro);
        }

        public List<HistorialModel> ObtenerHistorial() => historial;
    }
}
