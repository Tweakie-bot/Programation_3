
using System;

namespace Programation_3_DnD_Core
{
    public class InventoryState : IState
    {
        GameStateMachine _gameStateMachine;
        private GameManager _gameManager;
        private GameObject _playerGameObject;
        private IOutput _renderer;

        //
        public InventoryState(GameEngine engine, GameStateMachine machine,GameManager manager, IOutput render)
        {
            _gameStateMachine = machine;
            _gameManager = manager;
            _playerGameObject = _gameManager.GetPlayer();
            _renderer = render;
        }

        //
        public GameObject GetPlayer() { return _playerGameObject; }

        //
        public void Enter() { }
        public void Exit() { }
        public void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyQ())
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(PauseMenuState)));
            }
        }
        public void Update() { }
        public void FixedUpdate(float t) { }
        public void Render()
        {
            _renderer.RenderInventoryState(this);
        }

    }
}
