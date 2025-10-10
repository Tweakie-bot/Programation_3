using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class InGameState : IState
    {
        private StateMachine _machine;
        private GameObject _player;
        private GameManager _gameManager;

        private IOutput _renderer;

        private string _input;
        public InGameState(StateMachine machine, GameManager gameManager, IOutput renderer)
        {
            _machine = machine;
            _gameManager = gameManager;
            _player = _gameManager.GetPlayer();
            _renderer = renderer;
        }

        public void Enter()
        {
            _renderer.WriteLine("State : In Game");
        }

        public void Exit()
        {
            _renderer.WriteLine("Exiting : In Game");
        }

        public void Input(string input)
        {
            _input = input;
        }

        public void Update()
        {
            if (_input == "Escape")
            {
                _machine.SetState(_machine.GetPauseMenuState());
            }

            if (_player.GetComponent<LocationComponent>().GetLocation().CanWork() == true && _input == "W" && _gameManager.GetWorkStateMachine().IsWorkingHours() == true)
            {
                _machine.SetState(_machine.GetWorkingState());
            }

            if(_player.GetComponent<LocationComponent>().GetLocation().GetCanTrade() == true && _input == "T" && _gameManager.GetWorkStateMachine().IsWorkingHours() == true)
            {
                _gameManager.GetStateEventManager().Trigger(new StartTradingEvent());
            }

            if (int.TryParse(_input, out int number))
            {
                _player.Update(number);
            }

            _input = null;
        }

        public void Render()
        {
            _renderer.WriteLine("Press [ESCAPE] to pause the game");
            _renderer.WriteLine(" ");

            _player.GetComponent<LocationComponent>()?.Render();
            //Sécurité si le composant n'est pas initialisé mais devrait être inutilisé

            if (_player.GetComponent<LocationComponent>().GetLocation().CanWork() == true && _gameManager.GetWorkStateMachine().IsWorkingHours() == true)
            {
                _renderer.WriteLine("Press W to work for Mya");
            }

            if (_player.GetComponent<LocationComponent>().GetLocation().GetCanTrade() == true && _gameManager.GetWorkStateMachine().IsWorkingHours() == true)
            {
                _renderer.WriteLine("Press T to trade with Mya");
            }
        }
    }
}
