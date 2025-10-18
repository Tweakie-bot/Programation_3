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


namespace Programation_3_DnD.Engine
{
    internal class GameEngine
    {
        private IOutput _renderer;
        //Renderer de Program partagé par le gae engine au reste du jeu

        private ConsoleKey _lastKey;
        //Dernière entrée clavier

        private bool _shouldQuit = false;

        private GameStateMachine _stateMachine;

        private GameManager _gameManager;
        public GameEngine(IOutput renderer)
        {
            _renderer = renderer;

            _stateMachine = new GameStateMachine(this, renderer);

            _gameManager = new GameManager(_renderer, this);
        }

        public void Run()
        {
            while (!_shouldQuit)
            {
                ProcessInput();
                Update();
                Render();
            }
        }

        private void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                _lastKey = Console.ReadKey(true).Key;
            }

            if(_lastKey != ConsoleKey.None)
            {
                _stateMachine.ProcessInput(_lastKey);

                if (_stateMachine.GetCurrentState() is InGameState)
                {
                    _gameManager.ProcessInput(_lastKey);
                }
            }

            _lastKey = ConsoleKey.None;
        }

        private void Update()
        {
            if (_stateMachine.GetCurrentState() is InGameState)
            {
                _gameManager.Update();
            }
        }

        private void Render()
        {
            _stateMachine.Render();

            if (_stateMachine.GetCurrentState() is InGameState)
            {
                _gameManager.Render();
            }

            _renderer.Clear();
        }
    }
}
