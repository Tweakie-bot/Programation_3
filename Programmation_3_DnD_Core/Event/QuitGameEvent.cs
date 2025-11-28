using Programation_3_DnD.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Event
{
    public class QuitGameEvent : Event
    {
        //
        GameEngine _gameEngine;

        //
        public QuitGameEvent(GameEngine engine)
        {
            _gameEngine = engine;
        }

        //
        public override void Update()
        {
            _gameEngine.QuitGame();
        }
    }
}
