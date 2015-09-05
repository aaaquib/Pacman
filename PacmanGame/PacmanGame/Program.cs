using System;

namespace PacmanGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Pacman.Game1 game = new Pacman.Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

