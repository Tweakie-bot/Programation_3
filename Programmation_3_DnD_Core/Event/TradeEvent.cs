using System;
using System.Collections.Generic;
using System.Linq;

namespace Programation_3_DnD_Core
{
    public class TradeEvent : Event
    {
        //
        private GameStateMachine _gameStateMachine;
        private IOutput _renderer;
        private GameObject _playerGameObject;
        private GameObject _merchantGameObject;

        //
        public TradeEvent(GameStateMachine game_state_machine, IOutput renderer, GameObject player_game_object, GameObject merchant_game_object)
        {
            _gameStateMachine = game_state_machine;
            _renderer = renderer;
            _playerGameObject = player_game_object;
            _merchantGameObject = merchant_game_object;
        }

        //
        public override void Update()
        {
            if (_gameStateMachine == null) throw new Exception("StateMachine is null");
            if (_renderer == null) throw new Exception("Renderer is null");
            if (_playerGameObject == null) throw new Exception("Player is null");
            if (_merchantGameObject == null) throw new Exception("Merchant is null");

            _gameStateMachine.SetState(new TradingState(_gameStateMachine, _renderer, _playerGameObject, _merchantGameObject));
        }
    }
}
