using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class PauseMenuState : IState
    {
        private StateMachine _machine;
        private GameManager _gameManager;
        private string _input;

        private IOutput _renderer;

        public PauseMenuState(StateMachine machine, GameManager manager, IOutput renderer)
        {
            _machine = machine;
            _gameManager = manager;
            _renderer = renderer;
        }
        public void Enter()
        {
            _renderer.WriteLine("State : Pause menu");
        }

        public void Exit()
        {
            _renderer.WriteLine("Exiting : Pause menu");
        }

        public void Input(string input)
        {
            _input = input;
        }

        public void Update()
        {
            if (_input == "Escape")
            {
                _machine.SetState(_machine.GetMainMenuState());
                _input = null;
            }
            else if (_input == "I")
            {
                _gameManager.GetStateEventManager().Trigger(new OpenInventoryEvent(_gameManager.GetPlayer() as Player));
            }
        }

        public void Render()
        {
            _renderer.WriteLine("------     Pause menu     -----");
            _renderer.WriteLine(" ");
            _renderer.WriteLine("[ESCAPE] Main menu");
            _renderer.WriteLine("[I] Inventory");
        }
    }
}
