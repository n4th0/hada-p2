using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Coordenada
    {
        private int _Fila;
        public int Fila
        {

            get { return _Fila; }
            set {
                if (value < 0 || value > 9) { throw new ArgumentException("ERROR: range of the coordinate that has been introduce is not adecuate"); }
                _Fila = value; 
            }
        }
        private int _Columna;
        public int Columna
        {

            get { return _Columna; }
            set
            {
                if (value < 0 || value > 9) { throw new ArgumentException("ERROR: range of the coordinate that has been introduce is not adecuate"); }
                _Columna = value;
            }
        }

        // public int Fila { get; private set; }
        //public int Columna { get; private set; }
        public Coordenada()
        {
            _Fila = 0;
            _Columna = 0;
        }
        public Coordenada(int Fila, int Columna)
        {
            this.Fila = Fila;
            this.Columna = Columna;
        }
        public Coordenada(string Fila, string Columna)
        {
            // if (Int32.Parse(Columna) < 0 || Int32.Parse(Columna) > 9 || Int32.Parse(Fila) < 0 || Int32.Parse(Fila) > 9) { throw new ArgumentException(); }
            this.Fila = Int32.Parse(Fila);
            this.Columna = Int32.Parse(Columna);
        }
        public Coordenada(Coordenada cord)
        {
            this.Fila = cord.Fila;
            this.Columna = cord.Columna;
        }
        public override string ToString() { return "(" + Fila + "," + Columna + ")"; }

        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        public bool Equals(Coordenada other) { return this.Columna == other.Columna && this.Fila == other.Fila; }

        public override bool Equals(Object obj)
        {
            if (obj == null) { return false; }
            if (obj.GetType() != this.GetType()) { return false; }
            return this.Equals((Coordenada)obj);

        }
    }
}
