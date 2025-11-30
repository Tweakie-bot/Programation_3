
using System;

namespace Programation_3_DnD_Core
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
        public void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyTrade())
            {
                _eventManager.RegisterEvent(new TradeEvent(_gameStateMachine, _renderer, _playerGameObject, _merchantGameObject));
            }
        }
        public void Update() { }
        public void Render() { }
    }
}
