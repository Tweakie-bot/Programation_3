using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.StateTest
{
    public class MainMenuStateTest
    {
        private IOutput _renderer;
        private InputProcessor _inputProcessor;
        private GameEngine _engine;
        private EventManager _eventManager;
        private GameManager _gameManager;
        private GameStateMachine _gameStateMachine;
        private IState _state;

        [SetUp]
        public void Setup()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

            _renderer = new OutputManagerForTests();
            _inputProcessor = new InputProcessor();

            _engine = new GameEngine(_renderer, _inputProcessor, path);
            _eventManager = _engine.GetEventManager();
            _gameManager = _engine.GetGameManager();
            _gameStateMachine = _engine.GetGameStateMachine();

            _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(MainMenuState)));
            _state = _gameStateMachine.GetCurrentState();
        }

        [Test]
        public void TryEnterKeyGoToInGameState()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _state.TreatInput(_inputProcessor);

            Assert.IsInstanceOf<InGameState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryRandomKeyNoCrash()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A);
            _state.TreatInput(_inputProcessor);

            Assert.Pass();
        }

        [Test]
        public void TryRenderNoCrash()
        {
            _state.Render();
            Assert.Pass();
        }
    }
}

