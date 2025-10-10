using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class MainMenuState : IState
    {
        private StateMachine _stateMachine;

        private GameManager _gameManager;

        private IOutput _renderer;

        string _lastInput;

        public MainMenuState(StateMachine state_machine, GameManager game_manager, IOutput renderer)
        {
            _stateMachine = state_machine;
            _gameManager = game_manager;
            _renderer = renderer;
        }
        public void Enter()
        {
            _renderer.WriteLine("State : Main Menu");
            _renderer.Clear();
        }

        public void Exit()
        {
            _renderer.Clear();
            _renderer.WriteLine("Exiting : Main Menu");
        }

        public void Input(string input)
        {
            _lastInput = input;
        }

        public void Update()
        {
            if (_lastInput == "Enter")
            {
                _stateMachine.SetState(_stateMachine.GetInGameState());
                _lastInput = null;
            }
            else if (_lastInput == "Escape")
            {
                _gameManager.GetQuitGameEventManager().OnQuittingGame(new GameOverEvent("Player choose to quit"));
            }
        }

        public void Render()
        {
            _renderer.WriteLine("-----  MAIN MENU  -----");
            _renderer.WriteLine(" ");
            _renderer.WriteLine("[ENTER]  Play");
            _renderer.WriteLine("Click escape to quit");
        }
    }
}
