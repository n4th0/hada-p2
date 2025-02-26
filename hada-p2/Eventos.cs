using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class TocadoArgs : EventArgs
    {
        public string name;
        public Coordenada coordenadaImapcato;
        public TocadoArgs(string name, Coordenada coordenadaImpacto)
        {
            this.name = name;
            this.coordenadaImapcato = coordenadaImpacto;
        }
    }
    internal class HundidoArgs : EventArgs
    {
        public string name;
        public HundidoArgs(string name)
        {
            this.name = name;
        }
    }
}
