
using System;

namespace Programation_3_DnD_Core
{
    public class MainMenuState : IState
    {
        private GameEngine _gameEngine;
        private GameStateMachine _gameStateMachine;
        private IOutput _renderer;
        private EventManager _eventManager;

        //
        public MainMenuState(GameEngine game_engine, GameStateMachine state_machine, IOutput renderer, EventManager event_manager)
        {
            _gameEngine = game_engine;
            _gameStateMachine = state_machine;
            _renderer = renderer;
            _eventManager = event_manager;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyValidate())
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(InGameState)));
            }
            if (input_manager.IsKeyCancel())
            {
                _eventManager.RegisterEvent(new QuitGameEvent(_gameEngine));
            }
        }
        public void Update() { }
        public void FixedUpdate(float delta) { }
        public void Render()
        {
            _renderer.RenderMainMenuState(this);
        }
    }
}
