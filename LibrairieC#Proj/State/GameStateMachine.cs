
using System;
using System.Collections.Generic;

namespace Programation_3_DnD_Core
{
    public class GameStateMachine : IStateMachine
    {
        private GameEngine _gameEngine;
        private GameManager _gameManager;
        private EventManager _eventManager;
        private IOutput _renderer;
        private IState _currentState;
        private GameObject _player;

        private List<IState> _states;

        //
        public GameStateMachine(GameEngine game_engine, GameManager manager, IOutput renderer, EventManager event_manager)
        {
            _gameEngine = game_engine;
            _renderer = renderer;
            _gameManager = manager;
            _eventManager = event_manager;
            _player = _gameManager.GetPlayer();

            _states = new List<IState>();

            _states.Add(new MainMenuState(_gameEngine, this, _renderer, _eventManager));
            _states.Add(new InGameState(_gameEngine, this, _renderer, _player));
            _states.Add(new PauseMenuState(this, _gameEngine, _renderer));
            _states.Add(new InventoryState(game_engine, this, _gameManager, _renderer));

            _currentState = GetState(typeof(MainMenuState));
        }

        //
        public void SetState(IState current_state)
        {
            _currentState.Exit();

            _renderer.Clear();

            _currentState = current_state;
            _currentState.Enter();
        }

        //
        public IState GetState(Type type)
        {
            foreach (IState state in _states)
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
        //
        public void TreatInput(IInput input_manager)
        {
            _currentState.TreatInput(input_manager);
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
    }
}
