using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class TradingState : IState
    {
        private StateMachine _machine;
        private GameManager _gameManager;
        private string _input;

        private IOutput _renderer;

        public TradingState(StateMachine machine, GameManager manager, IOutput renderer)
        {
            _machine = machine;
            _gameManager = manager;
            _renderer = renderer;
        }
        public void Enter()
        {
            _renderer.WriteLine("You start trading with Mya");
        }

        public void Exit()
        {
            _renderer.WriteLine("Exiting trade");
        }

        public void Input(string input)
        {
            _input = input;
        }

        public void Update()
        {
            switch (_input)
            {
                case "1":
                    _renderer.WriteLine("You chose to trade items.");
                    break;
                case "Escape":
                    _machine.SetState(_machine.GetInGameState());
                    break;
            }
        }

        public void Render()
        {
            _renderer.WriteLine("Escape to go back");
            _renderer.WriteLine("Press 1 to trade");

            _gameManager.GetMya().GetComponent<InventoryComponent>().Render();
        }
    }
}
