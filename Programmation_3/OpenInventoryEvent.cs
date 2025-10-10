using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class OpenInventoryEvent : GameEvent
    {
        private Character _character;

        public Character GetCharacter() { return _character; }
        public OpenInventoryEvent(Character character)
        {
            _character = character;
        }
    }
}
