using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Coordenada c = new Coordenada { Fila = 4, Columna = 10};
            List<Barco> b = new List<Barco>();
            b.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
            b.Add(new Barco("LOKI", 2, 'v', new Coordenada(1, 2)));
            b.Add(new Barco("MAYA", 3, 'h', new Coordenada(3, 1)));

            // Console.WriteLine(b[1].ToString());

            Tablero t = new Tablero(4, b);

            Console.WriteLine(t.DibujarTablero());
            t.Disparar(new Coordenada(1, 2));
            t.Disparar(new Coordenada(2, 2));
            Console.WriteLine((b[1].hundido() ? "Sí" : "NO"));
            //Console.WriteLine("llego aqui");

            Console.WriteLine(t.DibujarTablero());


        }
    }
}
