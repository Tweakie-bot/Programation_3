
namespace Programation_3_DnD_Core
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
