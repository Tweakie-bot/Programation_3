using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.ComposantTest
{
    public class LocationComposantTest
    {
        private IOutput _renderer;
        private InputProcessor _inputProcessor;

        private GameEngine _engine;
        private EventManager _eventManager;
        private GameManager _gameManager;
        private GameStateMachine _machine;
        private GameObject _player;

        private LocationComposant _locationA;
        private LocationComposant _locationB;
        private LocationComposant _locationC;

        [SetUp]
        public void Setup()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");
            _inputProcessor = new InputProcessor();

            _renderer = new OutputManagerForTests();
            _engine = new GameEngine(_renderer, _inputProcessor, path);
            _eventManager = _engine.GetEventManager();
            _gameManager = _engine.GetGameManager();
            _machine = _engine.GetGameStateMachine();

            _player = _gameManager.GetPlayer();

            _locationA = new LocationComposant("Town", "Main town", _gameManager);
            _locationB = new LocationComposant("Forest", "Dark forest", _gameManager);
            _locationC = new LocationComposant("Cave", "Deep cave", _gameManager);

            _locationA.ConnectToNext(_locationB);
            _locationA.ConnectToNext(_locationC);

            _player.SetCurrentLocation(_locationA);
        }

        [Test]
        public void GettersReturnCorrectValues()
        {
            Assert.AreEqual("Town", _locationA.GetName());
            Assert.AreEqual("Main town", _locationA.GetDescription());
        }

        [Test]
        public void MoveToFirstLocation()
        {

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.D1);
            _locationA.TreatInput(_inputProcessor);

            PositionComposant position = _player.GetComposant<PositionComposant>();
            Assert.AreEqual("Forest", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void MoveToSecondLocation_WithKey2()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.D2);
            _locationA.TreatInput(_inputProcessor);

            PositionComposant position = _player.GetComposant<PositionComposant>();
            Assert.AreEqual("Cave", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void EscapeShouldReturnToPreviousLocation()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.D1);
            _locationA.TreatInput(new InputProcessor());

            PositionComposant position = _player.GetComposant<PositionComposant>();

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            position.GetCurrentLocation().TreatInput(_inputProcessor);

            Assert.AreEqual("Town", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void InvalidKeyDoesNotChangeLocation()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.D9);
            _locationA.TreatInput(new InputProcessor());

            PositionComposant position = _player.GetComposant<PositionComposant>();
            Assert.AreEqual("Town", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void CharacterReceivesInput()
        {
            GameObject npc = new GameObject();
            npc.AddComposant(new IDComposant("NPC"));

            _locationA.AddCharacter(npc);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A);
            Assert.DoesNotThrow(() => _locationA.TreatInput(_inputProcessor));
        }

        [Test]
        public void RenderDoesNotCrash()
        {
            Assert.DoesNotThrow(() => _locationA.Render());
        }

        [Test]
        public void UpdateDoesNotCrash()
        {
            Assert.DoesNotThrow(() => _locationA.Update());
        }

        [Test]
        public void FixedUpdateDoesNotCrash()
        {
            Assert.DoesNotThrow(() => _locationA.FixedUpdate(5f));
        }
    }
}
