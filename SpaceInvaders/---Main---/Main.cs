using System;
using System.Diagnostics;

// space invaders

namespace SpaceInvaders
{
    class M
    {
        static void Main(string[] args)
        {
            // Create the instance
            SpaceInvaders game = new SpaceInvaders();
            Debug.Assert(game != null);

            // Start the game
            game.Run();
        }
    }
}