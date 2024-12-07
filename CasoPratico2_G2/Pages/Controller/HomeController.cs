using Microsoft.AspNetCore.Mvc;

namespace CasoPratico2_G2.Pages.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private static readonly Dictionary<string, JuegoController> Juegos = new();
        private static readonly HistorialController Historial = new();

        [HttpPost("crear")]
        public IActionResult CrearJuego([FromBody] CrearJuegoRequest request)
        {
            var juego = new JuegoController(request.Jugador1, request.Jugador2, request.Color1, request.Color2);
            string juegoId = Guid.NewGuid().ToString();
            Juegos[juegoId] = juego;
            return Ok(new { juegoId });
        }

        [HttpPost("{id}/ficha")]
        public IActionResult InsertarFicha(string id, [FromBody] InsertarFichaRequest request)
        {
            if (!Juegos.TryGetValue(id, out var juegoController))
                return NotFound("Juego no encontrado.");

            if (!juegoController.InsertarFicha(request.Columna))
                return BadRequest("Columna llena o inválida.");

            var (finalizado, ganador) = juegoController.VerificarEstado();

            if (finalizado)
            {
                Historial.AgregarRegistro(
                    id,
                    request.Jugador1,
                    request.Jugador2,
                    ganador,
                    juegoController.ObtenerDuracion()
                );
            }

            return Ok(new
            {
                Tablero = juegoController.ObtenerTablero(),
                Finalizado = finalizado,
                Ganador = ganador
            });
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerJuego(string id)
        {
            if (!Juegos.TryGetValue(id, out var juegoController))
                return NotFound("Juego no encontrado.");

            return Ok(new
            {
                Tablero = juegoController.ObtenerTablero(),
                Duracion = juegoController.ObtenerDuracion()
            });
        }

        [HttpGet("historial")]
        public IActionResult ObtenerHistorial()
        {
            var historial = Historial.ObtenerHistorial();
            return Ok(historial);
        }
    }

    public record CrearJuegoRequest(string Jugador1, string Jugador2, string Color1, string Color2);
    public record InsertarFichaRequest(int Columna, string Jugador1, string Jugador2);
}
