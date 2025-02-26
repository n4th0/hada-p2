using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {

        private bool finPartida;

        public Game()
        { 
            finPartida = true;
            this.gameLoop();
        }

        private void gameLoop()
        {
            List<Barco> b = new List<Barco>();
            b.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
            b.Add(new Barco("LOKI", 2, 'v', new Coordenada(1, 2)));
            b.Add(new Barco("MAYA", 3, 'h', new Coordenada(3, 1)));
           

            // Console.WriteLine(b[1].ToString());

            Tablero t = new Tablero(4, b);
            t.eventoFinPartida += cuandoEventoFinPartida;
            string input = "";

            while (finPartida)
            {

                bool correctInput = false;
                do
                {
                    Console.WriteLine("Introduce la coordenada a la que tenga que disparar FILA,COLUMNA ('S' para Salir):");
                    input = Console.ReadLine();


                    if (input == "S" || input == "s") 
                    {
                        this.cuandoEventoFinPartida(this, new EventArgs()); 
                        correctInput = true;
                    }
                    else
                    {
                        string[] coordenadas = input.Split(',');
                        try
                        {
                            Coordenada c = new Coordenada(Int32.Parse(coordenadas[0]), Int32.Parse(coordenadas[1]));
                            t.Disparar(c);
                            if (finPartida)
                            {
                                Console.WriteLine(t.ToString());

                            }
                            correctInput = true;
                        }
                        catch (Exception ex)
                        {
                            // do nothing...

                        }


                    }

                }
                while (!correctInput);
            }
           
        }
        void cuandoEventoFinPartida(object obj, EventArgs e)
        {
            Console.WriteLine("GAME ENDED!!!!");
            finPartida = false;
        }

    }
}
