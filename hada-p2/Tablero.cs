using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Represents the game board, including ships, coordinates, and game logic.
    /// </summary>
    internal class Tablero
    {
        private int _TamTablero;

        /// <summary>
        /// Gets or sets the size of the game board. The size must be between 3 and 9 (inclusive).
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the value is outside the valid range [3, 9].</exception>
        public int TamTablero
        {
            get
            {
                return _TamTablero;
            }
            set
            {
                if (value < 3 || value > 9)
                {
                    throw new ArgumentException("ERROR: los limites del tablero deben ser [4,9]");
                }

                _TamTablero = value;
            }
        }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        /// <summary>
        /// Event triggered when the game ends.
        /// </summary>
        public event EventHandler<EventArgs> eventoFinPartida;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tablero"/> class with the specified size and list of ships.
        /// </summary>
        /// <param name="tamTablero">The size of the game board. Must be between 3 and 9 (inclusive).</param>
        /// <param name="barcos">The list of ships to place on the board.</param>
        /// <exception cref="ArgumentException">Thrown when the board size is outside the valid range [3, 9].</exception>
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            if (tamTablero < 3 || tamTablero > 9)
            {
                throw new ArgumentException();
            }
            TamTablero = tamTablero;

            this.barcos = barcos; // Shallow copy
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            // Subscribe to ship events
            foreach (var b in this.barcos)
            {
                b.eventoHundido += this.cuandoEventoHundido;
                b.eventoTocado += this.cuandoEventoTocado;
            }

            this.inicializaCasillasTablero();
        }

        /// <summary>
        /// Initializes the game board by setting up the grid and placing ships.
        /// </summary>
        private void inicializaCasillasTablero()
        {
            casillasTablero.Clear(); // Clears the board
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada c = new Coordenada(i, j);
                    casillasTablero.Add(c, "AGUA");
                }
            }

            // Place ships on the board
            foreach (Barco b in barcos)
            {
                Dictionary<Coordenada, string> casillasBarco = b.coordenadas;
                casillasBarco.Keys.ToList().ForEach(k => casillasTablero[k] = casillasBarco[k]);
            }
        }

        /// <summary>
        /// Fires a shot at the specified coordinate.
        /// </summary>
        /// <param name="c">The coordinate to fire at.</param>
        public void Disparar(Coordenada c)
        {
            if (c.Columna < 0 || c.Columna >= TamTablero || c.Fila < 0 || c.Fila >= TamTablero)
            {
                Console.WriteLine("The coordinate " + c.ToString() + " is outside the dimensions of the board");
            }
            else
            {
                coordenadasDisparadas.Add(c);
                foreach (Barco b in barcos)
                {
                    b.Disparo(c);
                }

                this.inicializaCasillasTablero();
                if (casillasTablero[c].EndsWith("_T") && !coordenadasTocadas.Contains(c))
                {
                    coordenadasTocadas.Add(c);
                }
            }
        }

        /// <summary>
        /// Draws the current state of the game board.
        /// </summary>
        /// <returns>A string representation of the game board.</returns>
        public string DibujarTablero()
        {
            int contador = 0;
            string tablero = "";
            foreach (Coordenada item in casillasTablero.Keys.ToList())
            {
                if (contador < item.Fila)
                {
                    tablero += "\n";
                    contador++;
                }

                tablero += "[" + casillasTablero[item] + "]";
            }

            return tablero;
        }

        /// <summary>
        /// Returns a string representation of the game board, including ships, fired coordinates, and hit coordinates.
        /// </summary>
        /// <returns>A string containing the game state.</returns>
        public override string ToString()
        {
            string output = "";
            foreach (Barco b in barcos)
            {
                output += b.ToString() + "\n";
            }
            output += "\nCoordenadas disparadas: ";
            foreach (Coordenada c in coordenadasDisparadas)
            {
                output += c.ToString() + " ";
            }

            output += "\nCoordenadas tocadas: ";
            foreach (Coordenada c in coordenadasTocadas)
            {
                output += c.ToString() + " ";
            }
            output += "\n\n\nCASILLAS TABLERO\n-------\n";
            output += this.DibujarTablero();
            output += "\n";
            return output;
        }

        /// <summary>
        /// Handles the event when a ship is hit.
        /// </summary>
        /// <param name="obj">The ship that was hit.</param>
        /// <param name="e">Event arguments containing the ship's name and the hit coordinate.</param>
        public void cuandoEventoTocado(object obj, TocadoArgs e)
        {
            Barco b = (Barco)obj;
            string s = "TABLERO: Barco [" + e.name + "] tocado en Coordenada: [" + e.coordenadaImapcato.ToString() + "]";
            if (b.hundido() && !barcosEliminados.Contains(b))
            {
                barcosEliminados.Add(b);
            }
            Console.WriteLine(s);
        }

        /// <summary>
        /// Handles the event when a ship is sunk.
        /// </summary>
        /// <param name="obj">The ship that was sunk.</param>
        /// <param name="e">Event arguments containing the ship's name.</param>
        public void cuandoEventoHundido(object obj, HundidoArgs e)
        {
            string s = "TABLERO: Barco [" + e.name + "] hundido!!";
            Console.WriteLine(s);

            // Check if all ships are sunk
            foreach (var ship in barcos)
            {
                if (!ship.hundido())
                {
                    return;
                }
            }

            // Trigger the end-of-game event
            this.eventoFinPartida(this, new EventArgs());
        }
    }
}
