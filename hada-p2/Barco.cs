using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Barco
    {
        public Dictionary<Coordenada, string> coordenadas { get; private set; }
        // public IReadOnlyDictionary<Coordenada, string> CoordenadasBarco { get; private set; }


        public string Nombre { get; private set; }

        public int Daños { get; private set; }

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Debes de escribir el nombre del barco");
            }

            if (longitud <= 0)
            {
                throw new ArgumentException("El barco no puede tener una longitud inferior a 1");
            }

            if (orientacion != 'v' && orientacion != 'h')
            {
                throw new ArgumentException("El barco tiene que estar orientado de forma horizontal (h) o vertical(v)");
            }

            if (coordenadaInicio == null)
            {
                throw new ArgumentException("Tiene que haber una coordenada de inicio");
            }

            Nombre = nombre;
            Daños = 0;
            coordenadas = new Dictionary<Coordenada, string>();

            for (int i = 0; i < longitud; i++)
            {
                Coordenada nuevaCoordenada;

                if (orientacion == 'h')
                {
                    nuevaCoordenada = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                }
                else
                {
                    nuevaCoordenada = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);
                }

                coordenadas[nuevaCoordenada] = Nombre;
            }
        }
        public void Disparo(Coordenada c)
        {
            if (coordenadas.ContainsKey(c))
            {
                string tag = coordenadas[c];
                if (!coordenadas[c].EndsWith("_T"))
                {
                    coordenadas[c] = coordenadas[c] + "_T";
                    Daños++;
                    
                    //Events?
                    /*if (hundido())
                    {

                    }
                    */
                    
                }
            }
        }

        public bool hundido()
        {
            foreach( var tag in coordenadas.Values){

                if (!tag.EndsWith("_T"))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string result = $"Barco: {Nombre}\nDaños: {Daños}\nHundido: {(hundido() ? "Sí" : "No")}\nCoordenadas:\n";
            foreach (var coord in coordenadas)
            {
                result += $"{coord.Key.ToString()} - {coord.Value}\n";
            }
            return result;
        }




    }
}
