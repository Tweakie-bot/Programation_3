using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class InventoryState : IState
    {
        private StateMachine _machine;
        private GameManager _gameManager;

        private string _lastInput;

        private IOutput _renderer;

        public InventoryState(StateMachine machine, GameManager game_manager, IOutput renderer)
        {
            _machine = machine;
            _gameManager = game_manager;
            _renderer = renderer;
        }

        public void Enter()
        {
            _renderer.WriteLine("State : Inventory");
        }

        public void Exit()
        {
            _renderer.WriteLine("End of inventory state");
        }

        public void Update()
        {
            if(_lastInput == "Escape")
            {
                _machine.SetState(_machine.GetPauseMenuState());
            }
        }

        public void Input (string input)
        {
            _lastInput = input;
        }

        public void Render()
        {
            _renderer.WriteLine("Press Escape to go back");
            _renderer.WriteLine(" ");
            if (0 < _gameManager.GetPlayer().GetComponent<InventoryComponent>().GetCount()) {
                _gameManager.GetPlayer().GetComponent<InventoryComponent>().Render();
            }
        }
    }
}
