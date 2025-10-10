using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class GainMoneyAchievementManager
    {
        private EventManager _eventManager;

        private IOutput _renderer;

        public GainMoneyAchievementManager(EventManager eventManager, IOutput renderer)
        {
            _eventManager = eventManager;
            _eventManager.RegisterToEvent<GainMoneyAchievementManager>(OnGainingGold);
            _renderer = renderer;
        }

        public void OnGainingGold(GameEvent game_event)
        {
            if (game_event as GainMoneyEvent != null)
            {
                _renderer.WriteLine($"{((GainMoneyEvent)game_event).GetCharacter().GetName()} gained : {((GainMoneyEvent)game_event).GetMoney()}");
            }
        }
    }
}
