using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;

namespace Programation_3_DnD.State
{
    internal class GameStateMachine : IStateMachine
    {
        private GameEngine _gameEngine;

        private IOutput _renderer;

        private IState _currentState;

        private List<IState> _states;
        public GameStateMachine(GameEngine game_engine, IOutput renderer)
        {
            _gameEngine = game_engine;

            _renderer = renderer;

            _states = new List<IState>();

            _states.Add(new MainMenuState(_gameEngine, this, _renderer));
            _states.Add(new InGameState(_gameEngine, this, _renderer));

            _currentState = GetState(typeof(MainMenuState));
        }

        public void ProcessInput(ConsoleKey key)
        {
            _currentState.ProcessInput(key);
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void FixedUpdate(float delta)
        {
            _currentState.FixedUpdate(delta);
        }

        public void Render()
        {
            _currentState.Render();
        }

        public void SetState(IState current_state)
        {
            _currentState = current_state;
        }
        public IState GetState(Type type)
        {
            foreach(IState state in _states)
            {
                if (state.GetType() == type)
                {
                    return state;
                }
            }
            throw new Exception("You tried to access a non existing state");
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }
    }
}
