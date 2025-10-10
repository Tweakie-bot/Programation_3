using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class GainMoneyEvent : GameEvent
    {
        private Character _character;
        private int _money;
        public GainMoneyEvent(Character character, int money)
        {
            _character = character;
            _money = money;
        }

        public Character GetCharacter()
        {
            return _character;
        }

        public int GetMoney()
        {
            return _money;
        }
    }
}
