
using System;

namespace Programation_3_DnD_Core
{
    public class PauseMenuState : IState
    {
        private GameStateMachine _gameStateMachine;
        private GameEngine _gameEngine;
        private IOutput _renderer;

        //
        public PauseMenuState(GameStateMachine state_machine, GameEngine game_engine, IOutput renderer) 
        { 
            _gameStateMachine = state_machine;
            _gameEngine = game_engine;
            _renderer = renderer;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyCancel())
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(MainMenuState)));
            }
            else if (input_manager.IsKeyPause())
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(InGameState)));
            }
            else if (input_manager.IsKeyInventory())
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(InventoryState)));

            }
        }
        public void Update() { }
        public void FixedUpdate(float dt) { }
        public void Render()
        {
            _renderer.RenderPauseMenuState(this);
        }
    }
}
