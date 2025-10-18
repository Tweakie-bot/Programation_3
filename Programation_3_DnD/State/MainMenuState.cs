using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;

namespace Programation_3_DnD.State
{
    internal class MainMenuState : IState
    {
        private GameEngine _gameEngine;
        private GameStateMachine _stateMachine;
        private IOutput _renderer;
        public MainMenuState(GameEngine gameEngine, GameStateMachine stateMachine, IOutput renderer)
        {
            _gameEngine = gameEngine;
            _stateMachine = stateMachine;
            _renderer = renderer;
        }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                _stateMachine.SetState(_stateMachine.GetState(typeof(InGameState)));
            }
        }

        public void Update()
        {

        }

        public void FixedUpdate(float delta)
        {

        }

        public void Render()
        {
            _renderer.WriteLine($"-------     [MAIN MENU]     -------");
            _renderer.PassLine();
            _renderer.WriteLine($"[ENTER] Play");
        }
    }
}
