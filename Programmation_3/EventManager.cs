using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class EventManager
    {
        private Dictionary < Type, List < Action < GameEvent > > > _eventTypeTable = new Dictionary<Type, List<Action<GameEvent>>> ();

        public void RegisterToEvent<TYPE>(Action<GameEvent> action)
        {
            Type type = typeof (TYPE);
         
            if (!_eventTypeTable.ContainsKey(type))
            {
                _eventTypeTable.Add(type, new List<Action<GameEvent>> ());
            }
            _eventTypeTable[type].Add (action);
        }

        public void TriggerEvent(GameEvent game_event)
        {
            Type type = game_event.GetType ();
            if (_eventTypeTable.ContainsKey(type))
            {
                foreach(Action<GameEvent> action in _eventTypeTable[type])
                {
                    action(game_event);
                }
            }
        }
    }
}
