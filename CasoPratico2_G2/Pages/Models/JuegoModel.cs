using Microsoft.AspNetCore.Components.Web;

namespace CasoPratico2_G2.Pages.Models
{
    public class JuegoModel
    {
        public string[,] Tablero { get; private set; }
        public string JugadorActual { get; private set; }
        public string ColorActual { get; private set; }
        public bool Finalizado { get; private set; }
        public string Ganador { get; private set; }

        private readonly string jugador1;
        private readonly string jugador2;
        private readonly string color1;
        private readonly string color2;

        public JuegoModel(string jugador1, string jugador2, string color1, string color2)
        {
            Tablero = new string[6, 7];
            this.jugador1 = jugador1;
            this.jugador2 = jugador2;
            this.color1 = color1;
            this.color2 = color2;
            JugadorActual = jugador1;
            ColorActual = color1;
            Finalizado = false;
            Ganador = null;

            InicializarTablero();

        }

        private void InicializarTablero()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                    Tablero[i, j] = ".";

        }

        public bool InsertarFicha(int columna)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (Tablero[i, columna] == ".")
            {
                Tablero[i, columna] = ColorActual;
                VerificarEstado(i, columna);
                CambioTurno();
                return true;

                }
            }
            return false;
        }

        private void CambioTurno()
        {
            if (!Finalizado)
            {
                JugadorActual = JugadorActual == jugador1 ? jugador2 : jugador1;
                ColorActual = ColorActual == color1 ? color2 : color2;
            }
        }
        private void VerificarEstado(int fila, int columna)
        {
            if (VerificarVictoria(fila, columna))
            {
                Finalizado = true;
                Ganador = JugadorActual;
            }
            else if(Empate())
            {
                Finalizado = true;
                Ganador = "Empate";

            }
        }

        private bool VerificarVictoria(int fila, int columna)
        {
            string ficha = Tablero[fila, columna];
            return VericarPosicion(fila, columna, 0, 1, ficha) ||
                   VericarPosicion(fila, columna, 1, 0, ficha) ||
                   VericarPosicion(fila, columna, 1, 1, ficha) ||
                   VericarPosicion(fila, columna, 1, -1, ficha);

        }

        private bool VericarPosicion(int fila, int col, int deltaFila, int deltaCol, string ficha)
        {
            int conteo = 0;
            for (int p = -3; p <= 3; p++)
            {

                int nuevaFila = fila + p * deltaFila;
                int nuevaCol = col + p * deltaCol;

                if (nuevaFila >= 0 && nuevaFila < 6 && nuevaCol >= 0 && nuevaCol < 7 && Tablero[nuevaFila, nuevaCol] == ficha)
                    conteo++;

                else
                    conteo = 0;

                if (conteo >= 4) return true;
            }

            return false;

        }

        private bool Empate()
        {
            foreach (string celda in Tablero)
                if (celda == ".") return false;
            return true;
        }

    }
}
