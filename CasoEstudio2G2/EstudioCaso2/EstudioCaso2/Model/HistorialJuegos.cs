namespace EstudioCaso2.Model
{
    public class HistorialJuegos
    {
        public static List<Juego> Juegos = new List<Juego>();

        public static void AgregarJuego(Juego juego)
        {
            Juegos.Add(juego);
        }

        public static List<Juego> ObtenerTodosLosJuegos()
        {
            return Juegos;
        }
    }
}