namespace EstudioCaso2.Model
{
    public class Juego
    {
        public string Jugador1 { get; set; }
        public string Jugador2 { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string JugadorActual { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string[] Tablero { get; set; }
        public bool JuegoTerminado { get; set; }
        public string Ganador { get; set; }

        public Juego(string jugador1, string jugador2, string color1, string color2)
        {
            Jugador1 = jugador1;
            Jugador2 = jugador2;
            Color1 = color1;
            Color2 = color2;
            JugadorActual = jugador1;
            Tablero = new string[42];
            JuegoTerminado = false;
            HoraInicio = DateTime.Now;
        }

        public bool RealizarMovimiento(int columna)
        {
            if (JuegoTerminado)
                return false;

            for (int fila = 5; fila >= 0; fila--)
            {
                int indice = fila * 7 + columna;
                if (string.IsNullOrEmpty(Tablero[indice]))
                {
                    Tablero[indice] = JugadorActual == Jugador1 ? Color1 : Color2;
                    VerificarGanador(fila, columna);
                    CambiarJugador();
                    return true;
                }
            }
            return false;
        }

        private void CambiarJugador()
        {
            JugadorActual = JugadorActual == Jugador1 ? Jugador2 : Jugador1;
        }

        private void VerificarGanador(int fila, int columna)
        {
            if (VerificarLinea(fila, columna, 1, 0) ||
                VerificarLinea(fila, columna, 0, 1) ||
                VerificarLinea(fila, columna, 1, 1) ||
                VerificarLinea(fila, columna, 1, -1))
            {
                JuegoTerminado = true;
                Ganador = JugadorActual;
                HoraFin = DateTime.Now;
            }
        }

        private bool VerificarLinea(int fila, int columna, int direccionFila, int direccionColumna)
        {
            string color = Tablero[fila * 7 + columna];
            int contador = 1;

            for (int i = 1; i < 4; i++)
            {
                int nuevaFila = fila + i * direccionFila;
                int nuevaColumna = columna + i * direccionColumna;

                if (nuevaFila >= 0 && nuevaFila < 6 && nuevaColumna >= 0 && nuevaColumna < 7 && Tablero[nuevaFila * 7 + nuevaColumna] == color)
                {
                    contador++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < 4; i++)
            {
                int nuevaFila = fila - i * direccionFila;
                int nuevaColumna = columna - i * direccionColumna;

                if (nuevaFila >= 0 && nuevaFila < 6 && nuevaColumna >= 0 && nuevaColumna < 7 && Tablero[nuevaFila * 7 + nuevaColumna] == color)
                {
                    contador++;
                }
                else
                {
                    break;
                }
            }

            return contador >= 4;
        }
    }
}