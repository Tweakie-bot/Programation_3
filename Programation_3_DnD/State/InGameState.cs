using Programation_3_DnD.Manager;
using Programation_3_DnD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Engine;

namespace Programation_3_DnD.State
{
    internal class InGameState : IState
    {
        private GameEngine _gameEngine;

        private IOutput _renderer;

        GameStateMachine _gameStateMachine;

        public InGameState(GameEngine game_engine, GameStateMachine state_machine, IOutput renderer)
        {
            _gameEngine = game_engine;
            _renderer = renderer;
            _gameStateMachine = state_machine;
        }

        public void ProcessInput(ConsoleKey key)
        {
            
        }

        public void Update()
        {
            
        }

        public void FixedUpdate(float d_t)
        {

        }

        public void Render()
        {

        }
    }
}
