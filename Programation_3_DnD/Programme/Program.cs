using Programation_3_DnD.Engine;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Interface;

class Program
{
    static void Main()
    {
        IOutput _renderer = new OutputManager();

        _renderer.WriteLine("Press any key except Q to play");

        ConsoleKey key = Console.ReadKey(true).Key;

        if (key != ConsoleKey.Q)
        {
            GameEngine engine = new GameEngine(_renderer);
            engine.Run();
        }

        _renderer.WriteLine("Thank you for playing");
    }
}