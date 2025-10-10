using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class StateEventManager
    {
        private EventManager _manager;
        private StateMachine _machine;

        public StateEventManager(EventManager manager, StateMachine machine)
        {
            _manager = manager;
            _machine = machine;

            _manager.RegisterToEvent<OpenInventoryEvent>(OnOpeningInventory);
            _manager.RegisterToEvent<StartTradingEvent>(OnTrading);
        }

        public void OnOpeningInventory(GameEvent ge)
        {
            _machine.SetState(_machine.GetInventoryState());
        }

        public void OnTrading(GameEvent ge)
        {
            _machine.SetState(_machine.GetTradingState());
        }

        public void Trigger(GameEvent ge)
        {
            _manager.TriggerEvent(ge);
        }
    }
}
