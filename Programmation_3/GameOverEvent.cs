using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class GameOverEvent : GameEvent
    {
        private string _reason;

        public GameOverEvent(string reason)
        {
            _reason = reason;
        }

        public string GetReason()
        {
            return _reason;
        }
    }
}
