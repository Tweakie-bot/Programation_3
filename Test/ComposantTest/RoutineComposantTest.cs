using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.ComposantTest
{
    public class RoutineComposantTest
    {
        private IOutput _renderer;
        private InputProcessor _inputProcessor;

        private GameEngine _engine;
        private EventManager _eventManager;
        private GameManager _gameManager;
        private GameStateMachine _stateMachine;

        private GameObject _player;
        private GameObject _npc;

        private EntityStateMachine _entityStateMachine;
        private RoutineComposant _routine;

        [SetUp]
        public void Setup()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

            _renderer = new OutputManagerForTests();
            _inputProcessor = new InputProcessor();

            _engine = new GameEngine(_renderer, _inputProcessor, path);
            _eventManager = _engine.GetEventManager();
            _gameManager = _engine.GetGameManager();
            _stateMachine = _engine.GetGameStateMachine();


            _player = _gameManager.GetPlayer();
            _npc = new GameObject();

            _entityStateMachine = new EntityStateMachine(_engine, _npc, _renderer, _eventManager, _player, _stateMachine);

            _entityStateMachine.EnableTrade();
            _entityStateMachine.EnableWork();

            _routine = new RoutineComposant(_entityStateMachine);
            _npc.AddComposant(_routine);
        }

        [Test]
        public void ProcessInputShouldNotCrash()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.T);
            Assert.DoesNotThrow(() => _routine.TreatInput(_inputProcessor));
        }

        [Test]
        public void UpdateShouldNotCrash()
        {
            Assert.DoesNotThrow(() => _routine.Update());
        }

        [Test]
        public void FixedUpdateShouldNotCrash()
        {
            Assert.DoesNotThrow(() => _routine.FixedUpdate(12f));
        }

        [Test]
        public void RenderShouldNotCrash()
        {
            Assert.DoesNotThrow(() => _routine.Render());
        }

        [Test]
        public void FullCycleShouldNotCrash()
        {
            Assert.DoesNotThrow(() =>
            {
                _inputProcessor.ChangeLastKeyForTests(ConsoleKey.W);
                _routine.TreatInput(_inputProcessor);
                _routine.FixedUpdate(10f);
                _routine.Update();
                _routine.Render();
            });
        }
    }
}
