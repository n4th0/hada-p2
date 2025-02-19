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

            Console.WriteLine("Fila: " + c.Fila);  
            Console.WriteLine("Columna: " + c.Columna);

        }
    }
}
