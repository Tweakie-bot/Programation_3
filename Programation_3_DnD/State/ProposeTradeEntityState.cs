using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.State
{
    public class ProposeTradeEntityState : IEntityState
    {
        private GameStateMachine _gameStateMachine;
        private GameObject _playerGameObject;
        private GameObject _merchantGameObject;
        private IOutput _renderer;
        private EventManager _eventManager;

        //
        public ProposeTradeEntityState(GameStateMachine state_machine, GameObject player, GameObject merchant, IOutput render, EventManager event_manager)
        {
            _renderer = render;
            _gameStateMachine = state_machine;
            _playerGameObject = player;
            _merchantGameObject = merchant;
            _eventManager = event_manager;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.T)
            {
                _eventManager.RegisterEvent(new TradeEvent(_gameStateMachine, _renderer, _playerGameObject, _merchantGameObject));
            }
        }
        public void Update() { }
        public void Render()
        {
            _renderer.WriteLine("Press T to trade");
        }
    }
}
