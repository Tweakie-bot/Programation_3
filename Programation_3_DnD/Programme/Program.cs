using Programation_3_DnD_Core;
using Programation_3_DnD_Console;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    static void Main()
    {
        IOutput _renderer = new OutputManager();
        IInput _inputProcessor = new InputProcessor();

        string _jsonPath = Path.Combine(AppContext.BaseDirectory, "Json");

        GameEngine engine = new GameEngine(_renderer, _inputProcessor, _jsonPath);

        Run(engine);

        _renderer.Clear();

        _renderer.WriteLine("Thank you for playing");
    }

    static void Run(GameEngine engine)
    {
        Stopwatch stop_watch = new Stopwatch();

        const float MAX_MS = 330f;

        float lag = 0;
        float last_time = 0;

        stop_watch.Start();

        while (!engine.GetShouldQuit())
        {
            float current_time = GetCurrentTime();
            float elapsed_time = current_time - last_time;

            lag += elapsed_time;

            engine.ProcessInput();

            while (lag >= MAX_MS)
            {
                engine.AddTime(MAX_MS / 1000);
                if (engine.GetTime() > 24)
                {
                    engine.AddTime(-24);
                }
                engine.FixedUpdate(engine.GetTime());
                lag -= MAX_MS;
            }

            engine.Update();
            engine.Render();

            last_time = current_time;
        }
        float GetCurrentTime()
        {
            if (stop_watch != null)
            {
                return stop_watch.ElapsedMilliseconds;
            }
            throw new Exception();
        }
    }
}