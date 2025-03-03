using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Represents a ship in the game with coordinates and damage tracking
    /// </summary>
    internal class Barco
    {
        /// <summary>
        /// Stores all ship coordinates and their status (normal or hit)
        /// </summary>
        public Dictionary<Coordenada, string> coordenadas { get; private set; }

        /// <summary>
        /// Event that triggers when the ship gets hit
        /// </summary>
        public event EventHandler<TocadoArgs> eventoTocado;

        /// <summary>
        /// Event that triggers when the ship is completely sunk
        /// </summary>
        public event EventHandler<HundidoArgs> eventoHundido;

        /// <summary>
        /// The ship's name (can't be empty)
        /// </summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Count of successful hits on the ship
        /// </summary>
        public int NumDanyos { get; private set; }

        /// <summary>
        /// Creates a new ship with specified position and size
        /// </summary>
        /// <param name="nombre">Ship name (required)</param>
        /// <param name="longitud">Ship length (must be at least 1)</param>
        /// <param name="orientacion">Placement direction: 'h' for horizontal, 'v' for vertical</param>
        /// <param name="coordenadaInicio">Starting position coordinates</param>
        /// <exception cref="ArgumentException">
        /// Throws error for invalid inputs: empty name, length 0, bad direction, or missing start position
        /// </exception>
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
            NumDanyos = 0;
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

        /// <summary>
        /// Handles a shot fired at this ship
        /// </summary>
        /// <param name="c">Coordinate being shot at</param>
        /// <remarks>
        /// - Marks hit coordinates with "_T"
        /// - Updates damage counter
        /// - Triggers hit/sunk events when needed
        /// </remarks>
        public void Disparo(Coordenada c)
        {
            if (coordenadas.ContainsKey(c))
            {
                if (!coordenadas[c].EndsWith("_T"))
                {
                    coordenadas[c] = coordenadas[c] + "_T";
                    NumDanyos++;

                    // Lanzar evento Tocado
                    this.eventoTocado(this, new TocadoArgs(this.Nombre, c));
                    // Console.WriteLine("llego aqui");


                    if (hundido())
                    {
                        this.eventoHundido(this, new HundidoArgs(this.Nombre));
                        // Console.WriteLine("llego aqui2");

                    }
                }
            }
        }

        /// <summary>
        /// Checks if the ship is completely destroyed
        /// </summary>
        /// <returns>True if all parts are hit, False otherwise</returns>
        public bool hundido()
        {
            foreach (var tag in coordenadas.Values)
            {
                if (!tag.EndsWith("_T"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Shows ship status in text format
        /// </summary>
        /// <returns>String with name, damage count, sunk status, and all coordinates</returns>
        public override string ToString()
        {

            string result = "[" + Nombre + "]" + " - DAÑOS: [" + NumDanyos + "] - HUNDIDO: [" + (hundido() ? "True" : "False") + "] - COORDENADAS: ";

            foreach (var coord in coordenadas)
            {
                result += "[" + coord.ToString() + " :" + Nombre + "] ";

            }

            return result;
        }
    }
}
