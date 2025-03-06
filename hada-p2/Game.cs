using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Represents the main game logic and flow.
    /// </summary>
    internal class Game
    {
        /// <summary>
        /// Indicates whether the game has ended.
        /// </summary>
        private bool finPartida;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class and starts the game loop.
        /// </summary>
        public Game()
        {
            finPartida = true;
            this.gameLoop();
        }

        /// <summary>
        /// Contains the main game loop where the game logic is executed.
        /// </summary>
        private void gameLoop()
        {
            // Create a list of ships and add them to the game.
            List<Barco> b = new List<Barco>();
            b.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
            b.Add(new Barco("LOKI", 2, 'v', new Coordenada(1, 2)));
            b.Add(new Barco("MAYA", 3, 'h', new Coordenada(3, 1)));

            // Create a game board and subscribe to the end-of-game event.
            Tablero t = new Tablero(4, b);
            t.eventoFinPartida += cuandoEventoFinPartida;

            string input = "";

            // Main game loop.
            while (finPartida)
            {
                bool correctInput = false;
                do
                {
                    // Prompt the user for input.
                    Console.WriteLine("Introduce la coordenada a la que tenga que disparar FILA,COLUMNA ('S' para Salir):");
                    input = Console.ReadLine();

                    // Handle exit command.
                    if (input == "S" || input == "s")
                    {
                        this.cuandoEventoFinPartida(this, new EventArgs());
                        correctInput = true;
                    }
                    else
                    {
                        // Parse and validate the input coordinates.
                        string[] coordenadas = input.Split(',');
                        try
                        {
                            Coordenada c = new Coordenada(Int32.Parse(coordenadas[0]), Int32.Parse(coordenadas[1]));
                            t.Disparar(c); // Fire at the specified coordinates.
                            if (finPartida)
                            {
                                Console.WriteLine(t.ToString()); // Display the updated game board.
                            }
                            correctInput = true;
                        }
                        catch (Exception ex)
                        {
                            // Silently handle invalid input.
                        }
                    }
                }
                while (!correctInput);
            }
        }

        /// <summary>
        /// Handles the end-of-game event.
        /// </summary>
        /// <param name="obj">The source of the event.</param>
        /// <param name="e">An object that contains no event data.</param>
        void cuandoEventoFinPartida(object obj, EventArgs e)
        {
            Console.WriteLine("GAME ENDED!!!!");
            finPartida = false; // End the game.
        }
    }
}