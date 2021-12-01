using System;

namespace DungeonGenerator
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new DungeonGenerator();
            game.Run();
        }
    }
}