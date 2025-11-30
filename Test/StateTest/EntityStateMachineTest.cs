
using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.StateTest
{
    public class EntityStateMachineTest
    {
        private IOutput _renderer;
        private InputProcessor _inputProcessor;

        private GameEngine _engine;
        private EventManager _eventManager;
        private GameStateMachine _gameStateMachine;
        private GameManager _gameManager;
        private GameObject _player;
        private GameObject _npc;
        private EntityStateMachine _entityStateMachine;

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

            _player = _gameManager.GetPlayer();
            _npc = new GameObject();

            _entityStateMachine = new EntityStateMachine(_engine, _npc, _renderer, _eventManager, _player, _gameStateMachine);
        }

        [Test]
        public void TryEnableTradeDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _entityStateMachine.EnableTrade(); });
        }

        [Test]
        public void TryEnableWorkDoesNotCrash()
        {
            Assert.DoesNotThrow(() => { _entityStateMachine.EnableWork(); });
        }

        [Test]
        public void TryProcessInputWithoutEnabledStates()
        {
            Assert.DoesNotThrow(() => 
            {
                _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A);
                _entityStateMachine.TreatInput(_inputProcessor);
            });
        }

        [Test]
        public void TryUpdateWithoutEnabledStates()
        {
            Assert.DoesNotThrow(() => { _entityStateMachine.Update(); });
        }

        [Test]
        public void TryRenderWithoutEnabledStates()
        {
            Assert.DoesNotThrow(() => { _entityStateMachine.Render(); });
        }

        [Test]
        public void DayTimeEnablesTradeAndWork()
        {
            _entityStateMachine.EnableTrade();
            _entityStateMachine.EnableWork();

            _entityStateMachine.FixedUpdate(10f);

            Assert.Pass();
        }

        [Test]
        public void NightTimeDisablesTradeAndWork()
        {
            _entityStateMachine.EnableTrade();
            _entityStateMachine.EnableWork();

            _entityStateMachine.FixedUpdate(2f);

            Assert.Pass();
        }

        [Test]
        public void TrySwitchMultipleTimes()
        {
            _entityStateMachine.EnableTrade();
            _entityStateMachine.EnableWork();

            _entityStateMachine.FixedUpdate(10f);
            _entityStateMachine.FixedUpdate(22f);
            _entityStateMachine.FixedUpdate(8f);

            Assert.Pass();
        }
    }
}
