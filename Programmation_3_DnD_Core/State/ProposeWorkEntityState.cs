using Programation_3_DnD.Engine;
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
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.W)
            {
                _eventManager.RegisterEvent(new WorkEvent(_engine, _playerGameObject, 10, _renderer, _eventManager));
            }
        }
        public void Update() { }
        public void Render() { }
    }
}
