using System.Diagnostics;
using System;
using System.Collections.Generic;


namespace Programation_3_DnD_Core
{
    public class GameEngine
    {
        //
        private IOutput _renderer;
        private IInput _inputProcessor;

        private bool _shouldQuit = false;

        private GameStateMachine _gameStateMachine;
        private GameManager _gameManager;
        private EventManager _eventManager;

        private float _time;

        private List<string> _uiMessages = new List<string>();

        private string _path;

        //
        public GameEngine(IOutput renderer, IInput inputProcessor, string path)
        {
            _inputProcessor = inputProcessor;

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
        public void AddTime(float time_to_add)
        {
            _time += time_to_add;
        }

        //
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
        public bool GetShouldQuit() { return _shouldQuit; }
        public float GetTime() { return _time; }
        //
        public void ProcessInput()
        {
            _inputProcessor.ProcessInput();

            if(!_inputProcessor.IsKeyNone())
            {
                _gameStateMachine.TreatInput(_inputProcessor);

                if (_gameStateMachine.GetCurrentState() is InGameState)
                {
                    _gameManager.TreatInput(_inputProcessor);
                }
            }
            _inputProcessor.ResetInput();
        }
        public void Update()
        {
            _eventManager.Update();

            _gameStateMachine.Update();

            if (_gameStateMachine.GetCurrentState() is InGameState)
            {
                _gameManager.Update();
            }
        }
        public void FixedUpdate(float time)
        {
            _gameStateMachine.FixedUpdate(time);

            if (_gameStateMachine.GetCurrentState() is InGameState)
            {
                _gameManager.FixedUpdate(time);
            }
        }
        public void Render()
        {
            _renderer.BeginFrame();

            _renderer.Clear();
            _gameStateMachine.Render();

            if (_gameStateMachine.GetCurrentState() is InGameState)
            {
                _gameManager.Render();
            }

            _renderer.EndFrame();
        }
    }
}
