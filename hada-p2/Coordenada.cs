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
        public int Fila { get; private set; }
        public int Columna { get; private set; }
        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }
        public Coordenada(int Fila, int Columna)
        {
            if (Columna < 0 || Columna > 9 || Fila < 0 || Fila > 9) { throw new ArgumentException(); }
            this.Fila = Fila;
            this.Columna = Columna;
        }
        public Coordenada(string Fila, string Columna)
        {
            if (Int32.Parse(Columna) < 0 || Int32.Parse(Columna) > 9 || Int32.Parse(Fila) < 0 || Int32.Parse(Fila) > 9) { throw new ArgumentException(); }
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
