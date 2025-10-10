using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class QuitGameEventManager
    {
        private EventManager _eventManager;
        private bool _shouldQuitGame;

        private IOutput _renderer;
        public bool GetShouldQuitGame() => _shouldQuitGame;
        public QuitGameEventManager(EventManager eventManager, IOutput renderer)
        {
            _eventManager = eventManager;
            _eventManager.RegisterToEvent<GameOverEvent>(OnQuittingGame);
            _renderer = renderer;
        }

        public void OnQuittingGame(GameEvent game_event)
        {
            _shouldQuitGame = true;
            _renderer.WriteLine("Closing application");
            _renderer.WriteLine((game_event as GameOverEvent).GetReason());
        }
    }
}
