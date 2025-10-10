using Programmation_3;

class Program
{
    static void Main()
    {
        IOutput renderer = new OutputRenderer();

        renderer.WriteLine("Press any key to play except Q");

        ConsoleKey key = Console.ReadKey().Key;

        if (key != ConsoleKey.Q)
        {
            GameManager manager = new GameManager(renderer);
            manager.Run();
        }
    }
}
