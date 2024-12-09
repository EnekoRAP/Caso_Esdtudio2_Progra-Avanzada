using EstudioCaso2.Model;
using Microsoft.AspNetCore.Mvc;

namespace EstudioCaso2.Controladores
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuegoController : ControllerBase
    {
        [HttpPost("crear")]
        public IActionResult CrearJuego([FromBody] SolicitudJuego solicitud)
        {
            var juego = new Juego(solicitud.Jugador1, solicitud.Jugador2, solicitud.Color1, solicitud.Color2);
            HistorialJuegos.AgregarJuego(juego);
            return Ok(juego);
        }

        [HttpPost("{idJuego}/mover")]
        public IActionResult RealizarMovimiento(int idJuego, [FromBody] SolicitudMovimiento solicitudMovimiento)
        {
            var juego = HistorialJuegos.Juegos.ElementAtOrDefault(idJuego);
            if (juego == null || juego.JuegoTerminado)
                return BadRequest("Juego no encontrado o ya finalizado.");

            var exito = juego.RealizarMovimiento(solicitudMovimiento.Columna);
            if (!exito)
                return BadRequest("Movimiento inválido.");

            return Ok(juego);
        }

        [HttpGet("historial")]
        public IActionResult ObtenerHistorialJuegos()
        {
            var juegos = HistorialJuegos.ObtenerTodosLosJuegos();
            return Ok(juegos);
        }
    }
}
