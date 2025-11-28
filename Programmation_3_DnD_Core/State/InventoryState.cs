using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.State
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
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Q)
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
