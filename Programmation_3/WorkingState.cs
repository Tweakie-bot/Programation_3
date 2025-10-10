using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class WorkingState : IState
    {
        private StateMachine _state;
        private GameManager _gameManager;
        private string _input;
        private int _efficiency = 0;

        private IOutput _renderer;

        public WorkingState(StateMachine machine, GameManager gameManager, IOutput render)
        {
            _state = machine;
            _gameManager = gameManager;
            _renderer = render;
        }
        public void Enter()
        {
            _renderer.WriteLine("State : Working");
        }

        public void Exit()
        {
            _renderer.WriteLine("You finished working");
        }

        public void Input(string input)
        {
            _input = input;
        }

        public void Update()
        {
            if (_input == "F")
            {
                if (! _gameManager.GetPlayer().GetComponent<InventoryComponent>().Contains("Gold"))
                {
                    _gameManager.GetPlayer().GetComponent<InventoryComponent>().Add(new Item("Gold", "Shiny yellow friend", 10 + _efficiency));
                }

                else
                {
                    _gameManager.GetPlayer().GetComponent<InventoryComponent>().AddQuantity("Gold", 10 + _efficiency);
                }
                _gameManager.GetMoneyAchievementManager().OnGainingGold(new GainMoneyEvent((Character)_gameManager.GetPlayer(), 10 + _efficiency));

                _efficiency += 5;
                _state.SetState(_state.GetInGameState());
            }
        }

        public void Render()
        {
            _renderer.WriteLine("[F]  Work");
        }
    }
}
