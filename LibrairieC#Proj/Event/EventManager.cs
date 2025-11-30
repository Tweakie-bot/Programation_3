
using System.Collections.Generic;

namespace Programation_3_DnD_Core
{
    public class EventManager
    {
        //
        private Queue<Event> _events = new Queue<Event>();

        //
        public void RegisterEvent(Event e)
        {
            _events.Enqueue(e);
        }

        //
        public void Update()
        {
            int count = _events.Count;

            int event_count = _events.Count;
            while (_events.Count > 0 && event_count-- > 0)
            {
                Event current_event = _events.Dequeue();
                current_event.Update();

                if (current_event is TimedEvent)
                {
                    if (!((TimedEvent)current_event).GetIsCompleted())
                    {
                        _events.Enqueue(current_event);
                    }
                }
            }
        }
    }
}
