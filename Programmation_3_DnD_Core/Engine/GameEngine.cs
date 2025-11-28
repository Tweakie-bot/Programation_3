using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using Programation_3_DnD.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Event;
using Spectre.Console;


namespace Programation_3_DnD.Engine
{
    public class GameEngine
    {
        //
        private IOutput _renderer;

        private ConsoleKey _lastKey;

        private bool _shouldQuit = false;

        private GameStateMachine _gameStateMachine;
        private GameManager _gameManager;
        private EventManager _eventManager;

        Stopwatch _stopWatch;

        private float _time;

        private List<string> _uiMessages = new();

        private string _path;

        //
        public GameEngine(IOutput renderer, string path)
        {
            _stopWatch = new Stopwatch();

            _path = path;

            _renderer = renderer;

            _eventManager = new EventManager();

            _gameManager = new GameManager(_renderer, this, _eventManager, _path);

            _gameStateMachine = new GameStateMachine(this, _gameManager, renderer, _eventManager);

            _gameManager.AddStateMachine(_gameStateMachine);
        }

        //
        public void QuitGame()
        {
            _shouldQuit = true;
        }
        public void Run()
        {
            const float MAX_MS = 330f;

            float lag = 0;

            float last_time = 0;

            _stopWatch.Start();

            while (!_shouldQuit)
            {
                float current_time = GetCurrentTime();
                float elapsed_time = current_time - last_time;

                lag += elapsed_time;

                ProcessInput();

                while (lag >= MAX_MS)
                {
                    _time += MAX_MS / 1000;
                    if (_time > 24)
                    {
                        _time -= 24;
                    }
                    FixedUpdate(_time);
                    lag -= MAX_MS;
                }

                Update();
                Render();

                last_time = current_time;
            }
        }
        public void Work()
        {
            _time += 6;
        }
        public void ClearUIMessages()
        {
            _uiMessages.Clear();
        }
        public void PushUIMessage(string message)
        {
            _uiMessages.Add(message);
        }

        //
        private float GetCurrentTime()
        {
            return (float)_stopWatch.Elapsed.TotalMilliseconds;
        }
        public IReadOnlyList<string> GetUIMessages()
        {
            return _uiMessages;
        }
        public GameStateMachine GetGameStateMachine()
        {
            return _gameStateMachine;
        }
        public GameManager GetGameManager()
        {
            return _gameManager;
        }
        public EventManager GetEventManager()
        {
            return _eventManager;
        }

        //
        private void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                _lastKey = Console.ReadKey(true).Key;
            }

            if(_lastKey != ConsoleKey.None)
            {
                _gameStateMachine.ProcessInput(_lastKey);

                if (_gameStateMachine.GetCurrentState() is InGameState)
                {
                    _gameManager.ProcessInput(_lastKey);
                }
            }

            _lastKey = ConsoleKey.None;
        }
        private void Update()
        {
            _eventManager.Update();

            _gameStateMachine.Update();

            if (_gameStateMachine.GetCurrentState() is InGameState)
            {
                _gameManager.Update();
            }
        }
        private void FixedUpdate(float time)
        {
            _gameStateMachine.FixedUpdate(time);

            if (_gameStateMachine.GetCurrentState() is InGameState)
            {
                _gameManager.FixedUpdate(time);
            }
        }
        private void Render()
        {
            _renderer.BeginFrame();

            AnsiConsole.Clear();
            _gameStateMachine.Render();

            _renderer.EndFrame();
        }
    }
}
