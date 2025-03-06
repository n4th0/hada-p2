using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Provides data for the Tocado event.
    /// </summary>
    internal class TocadoArgs : EventArgs
    {
        /// <summary>
        /// The name of the object that was hit.
        /// </summary>
        public string name;

        /// <summary>
        /// The coordinates of the impact.
        /// </summary>
        public Coordenada coordenadaImapcato;

        /// <summary>
        /// Initializes a new instance of the <see cref="TocadoArgs"/> class.
        /// </summary>
        /// <param name="name">The name of the object that was hit.</param>
        /// <param name="coordenadaImpacto">The coordinates of the impact.</param>
        public TocadoArgs(string name, Coordenada coordenadaImpacto)
        {
            this.name = name;
            this.coordenadaImapcato = coordenadaImpacto;
        }
    }

    /// <summary>
    /// Provides data for the Hundido event.
    /// </summary>
    internal class HundidoArgs : EventArgs
    {
        /// <summary>
        /// The name of the object that was sunk.
        /// </summary>
        public string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="HundidoArgs"/> class.
        /// </summary>
        /// <param name="name">The name of the object that was sunk.</param>
        public HundidoArgs(string name)
        {
            this.name = name;
        }
    }
}