using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Represents a coordinate with row and column values within a 0-9 range.
    /// </summary>
    internal class Coordenada
    {
        private int _Fila;
        /// <summary>
        /// Gets or sets the row component of the coordinate
        /// </summary>
        /// <value>
        /// Integer value between 0 and 9 
        /// </value>
        /// <exception cref="ArgumentException">
        /// Thrown when value is less than 0 or greater than 9
        /// </exception>
        public int Fila
        {
            get { return _Fila; }
            set
            {
                if (value < 0 || value > 9) { throw new ArgumentException("ERROR: range of the coordinate that has been introduce is not adecuate"); }
                _Fila = value;
            }
        }

        private int _Columna;
        /// <summary>
        /// Gets or sets the column component of the coordinate
        /// </summary>
        /// <value>
        /// Integer value between 0 and 9 
        /// </value>
        /// <exception cref="ArgumentException">
        /// Thrown when value is less than 0 or greater than 9
        /// </exception>
        public int Columna
        {
            get { return _Columna; }
            set
            {
                if (value < 0 || value > 9) { throw new ArgumentException("ERROR: range of the coordinate that has been introduce is not adecuate"); }
                _Columna = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordenada"/> class with default (0,0) coordinates
        /// </summary>
        public Coordenada()
        {
            _Fila = 0;
            _Columna = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordenada"/> class with specified coordinates
        /// </summary>
        /// <param name="Fila">Row value (0-9)</param>
        /// <param name="Columna">Column value (0-9)</param>
        /// <exception cref="ArgumentException">
        /// Thrown when either parameter is out of the valid range
        /// </exception>
        public Coordenada(int Fila, int Columna)
        {
            this.Fila = Fila;
            this.Columna = Columna;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordenada"/> class from string representations
        /// </summary>
        /// <param name="Fila">String representation of row value</param>
        /// <param name="Columna">String representation of column value</param>
        /// <exception cref="ArgumentException">
        /// Thrown when parsed values are out of valid range
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown when input strings cannot be parsed to integers
        /// </exception>
        public Coordenada(string Fila, string Columna)
        {
            this.Fila = Int32.Parse(Fila);
            this.Columna = Int32.Parse(Columna);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordenada"/> class as a copy of another instance
        /// </summary>
        /// <param name="cord">Coordinate to copy</param>
        public Coordenada(Coordenada cord)
        {
            this.Fila = cord.Fila;
            this.Columna = cord.Columna;
        }

        /// <summary>
        /// Returns a string representation of the coordinate
        /// </summary>
        /// <returns>String in format (Fila,Columna)</returns>
        public override string ToString() { return "(" + Fila + "," + Columna + ")"; }

        /// <summary>
        /// Returns a hash code for the current coordinate
        /// </summary>
        /// <returns>Combined hash code of row and column values</returns>
        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        /// <summary>
        /// Determines if its equals to another coordenada instance
        /// </summary>
        /// <param name="other">Coordinate to compare</param>
        /// <returns>True if both coordinates have identical values, false otherwise</returns>
        public bool Equals(Coordenada other) { return this.Columna == other.Columna && this.Fila == other.Fila; }

        /// <summary>
        /// Determines =? to another object
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>
        /// True if obj is a Coordenada with identical values, false otherwise
        /// </returns>
        public override bool Equals(Object obj)
        {
            if (obj == null) { return false; }
            if (obj.GetType() != this.GetType()) { return false; }
            return this.Equals((Coordenada)obj);
        }
    }
}