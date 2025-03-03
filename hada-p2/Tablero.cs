using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        private int _TamTablero;
        public int TamTablero { 
            get {
                return _TamTablero;
            }
            set {
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
        public event EventHandler<EventArgs> eventoFinPartida;

        public Tablero(int tamTablero, List<Barco> barcos)
        {

            if (tamTablero < 3 || tamTablero > 9)
            {
                throw new ArgumentException();
            }
            TamTablero = tamTablero;

        

            
            this.barcos = barcos; // shallow copy 
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();
            foreach (var b in this.barcos)
            {
                b.eventoHundido += this.cuandoEventoHundido;
                b.eventoTocado += this.cuandoEventoTocado;
            }
            this.inicializaCasillasTablero();
        }

        private void inicializaCasillasTablero()
        {
            casillasTablero.Clear(); // invoca al destructor de coordenada y de string
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada c = new Coordenada(i, j);
                    casillasTablero.Add(c, "AGUA");
                }
            }
            foreach (Barco b in barcos)
            {
                // IReadOnlyDictionary<Coordenada, string> casillasBarco = b.CoordenadasBarco;
                Dictionary<Coordenada, string> casillasBarco = b.coordenadas;
                casillasBarco.Keys.ToList().ForEach(k => casillasTablero[k] = casillasBarco[k]);
            }
        }

        public void Disparar(Coordenada c) { 
            if(c.Columna < 0 || c.Columna >= TamTablero || c.Fila < 0 || c.Fila >= TamTablero) { 
                Console.WriteLine("The coordinate "+ c.ToString() + " is outside the dimensions of the board");
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


        public string DibujarTablero()
        {
            int contador = 0;
            string tablero = "";
            foreach (Coordenada item in casillasTablero.Keys.ToList())
            {
                if(contador < item.Fila)
                {
                    tablero += "\n";
                    contador++;
                }

                tablero += "["+casillasTablero[item]+"]";
            }

            return tablero;
        }

        public override string ToString()
        {
            string output = "";
            foreach(Barco b in barcos)
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
            output  += "\n\n\nCASILLAS TABLERO\n-------\n";
            output += this.DibujarTablero();
            output += "\n";
            return output;
        }

        public void cuandoEventoTocado(object obj, TocadoArgs e)
        {
            Barco b = (Barco)obj;
            string s = "TABLERO: Barco [" + e.name + "] tocado en Coordenada: [" + e.coordenadaImpacato.ToString() + "]";
            if(b.hundido() && !barcosEliminados.Contains(b)) { barcosEliminados.Add(b); 
            
            }
            Console.WriteLine(s);

        }
        public void cuandoEventoHundido(object obj, HundidoArgs e)
        {
            // Barco b = (Barco)obj;
            string s = "TABLERO: Barco [" + e.name + "] hundido!!";
            Console.WriteLine(s);
            foreach (var ship in barcos)
            {
                if (!ship.hundido())
                {
                    return;
                }
            }
            // the game is ended so we call the event to end the game
            // Console.WriteLine("llego al final");
            this.eventoFinPartida(this, new EventArgs());

        }

    }
}
