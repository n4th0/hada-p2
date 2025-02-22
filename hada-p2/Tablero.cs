using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        int TabTablero { get; private set; }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public Tablero(int tamTablero, List<Barco> barcos)
        {

            if (tamTablero < 3 || tamTablero > 9)
            {
                throw new ArgumentException();
            }
            TabTablero = tamTablero;

            this.barcos = barcos; // shallow copy 
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            this.inicializaCasillasTablero();
        }

        private void inicializaCasillasTablero()
        {
            casillasTablero.Clear(); // invoca al destructor de coordenada y de string
            for (int i = 0; i < TabTablero; i++)
            {
                for (int j = 0; j < TabTablero; j++)
                {
                    Coordenada c = new Coordenada(i, j);
                    casillasTablero.Add(c, "AGUA");
                }
            }
            foreach (Barco b in barcos)
            {
                Dictionary<Coordenada, string> casillasBarco = b.getCoordenadasBarco();
                casillasBarco.Keys.ToList().ForEach(k => casillasTablero[k] = casillasBarco[k]);
            }
        }

        public void Disparar(Coordenada c) { 
            if(c.Columna < 0 || c.Columna > TabTablero || c.Fila < 0 || c.Fila > TabTablero) { 
                Console.WriteLine("The coordinate "+ c.ToString() + "is outside the dimensions of the board");
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
            string tablero = "";
            for (int i = 0; i < TabTablero; i++)
            {
                for (int j = 0; j < TabTablero; j++)
                {
                    tablero += "["+casillasTablero[new Coordenada(i, j)]+"]";
                }
                tablero += "\n";
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
            output  += "\n\n\nCASILLAS TABLERO\n-------";
            output += this.DibujarTablero();
            output += "\n";
            return output;
        }


        // TODO - implementar eventos
    }
}
