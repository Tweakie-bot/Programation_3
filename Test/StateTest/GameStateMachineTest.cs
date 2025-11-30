
using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.StateTest
{
    public class GameStateMachineTest
    {
        private IOutput _renderer;
        private InputProcessor _inputProcessor;

        private GameEngine _engine;
        private EventManager _eventManager;
        private GameManager _gameManager;
        private GameStateMachine _gameStateMachine;

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
        }

        [Test]
        public void DefaultStateIsMainMenu()
        {
            Assert.IsInstanceOf<MainMenuState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void GetStateReturnsCorrectInstance()
        {
            IState state = _gameStateMachine.GetState(typeof(InGameState));
            Assert.IsInstanceOf<InGameState>(state);
        }

        [Test]
        public void SetStateChangesCurrentState()
        {
            IState new_state = _gameStateMachine.GetState(typeof(InGameState));
            _gameStateMachine.SetState(new_state);

            Assert.IsInstanceOf<InGameState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void ProcessInputDelegatesToCurrentState()
        {
            Assert.DoesNotThrow(() => 
            {
                _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A);
                _gameStateMachine.TreatInput(_inputProcessor);
            });
        }

        [Test]
        public void UpdateDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _gameStateMachine.Update(); });
        }

        [Test]
        public void FixedUpdateDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _gameStateMachine.FixedUpdate(1.0f); });
        }

        [Test]
        public void RenderDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _gameStateMachine.Render(); });
        }

        [Test]
        public void GetStateWithWrongTypeThrowsException()
        {
            Assert.Throws<Exception>(() => { _gameStateMachine.GetState(typeof(TradingState)); });
        }
    }
}
