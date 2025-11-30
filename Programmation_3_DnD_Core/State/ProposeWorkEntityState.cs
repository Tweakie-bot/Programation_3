
using System;

namespace Programation_3_DnD_Core
{
    public class ProposeWorkEntityState : IEntityState
    {
        GameEngine _engine;
        private IOutput _renderer;
        private GameObject _playerGameObject;
        private EventManager _eventManager;

        //
        public ProposeWorkEntityState(GameEngine engine, IOutput output, EventManager event_manager, GameObject player)
        {
            _engine = engine;
            _renderer = output;
            _eventManager = event_manager;
            _playerGameObject = player;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyWork())
            {
                _eventManager.RegisterEvent(new WorkEvent(_engine, _playerGameObject, 10, _renderer, _eventManager));
            }
        }
        public void Update() { }
        public void Render() { }
    }
}
