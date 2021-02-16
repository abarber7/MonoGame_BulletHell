using System;

namespace BulletHell
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BulletHell())
                game.Run();
        }
    }
}
