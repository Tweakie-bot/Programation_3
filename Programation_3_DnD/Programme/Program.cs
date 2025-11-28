using Programation_3_DnD.Engine;
using Programation_3_DnD.Output;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Interface;

internal class Program
{
    static void Main()
    {
        IOutput _renderer = new OutputManager();

        string _jsonPath = Path.Combine(AppContext.BaseDirectory, "Json");

        GameEngine engine = new GameEngine(_renderer, _jsonPath);
        
        engine.Run();

        _renderer.Clear();

        _renderer.WriteLine("Thank you for playing");
    }
}