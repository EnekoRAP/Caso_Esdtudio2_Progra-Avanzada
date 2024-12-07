using CasoPratico2_G2.Pages.Models;

namespace CasoPratico2_G2.Pages.Controller
{
    public class JuegoController
    {
        private readonly Juego juego;
        private readonly DateTime comenzar;


        public JuegoController(string jugador1, string jugador2, string color1, string color2)
        {
            juego = new Juego(jugador1, color1, jugador2, color2);
            comenzar = DateTime.Now;

        }

        public string[,] ObtenerTablero() => juego.Tablero;

        public bool InsertarFicha(int columna) => juego.InsertarFicha(columna);

        public (bool finalizado, string ganador) VerificarEstado()
        {
            if (juego.Finalizado)
            {

                return (true, juego.Ganador);

            }
            return (false, null);
        }

        public TimeSpan ObtenerDuracion()
        {
            return DateTime.Now - comenzar;
        }


    }

}
