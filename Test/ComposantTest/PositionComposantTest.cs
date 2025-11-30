using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.ComposantTest
{
    public class PositionComposantTest
    {
        private IOutput _renderer;
        private InputProcessor _inputProcessor;
        private GameEngine _engine;
        private EventManager _eventManager;
        private GameManager _gameManager;
        private GameStateMachine _stateMachine;

        private GameObject _player;
        private GameObject _locationObjectA;
        private GameObject _locationObjectB;

        private LocationComposant _locationA;
        private LocationComposant _locationB;

        private PositionComposant _position;

        [SetUp]
        public void Setup()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

            _inputProcessor = new InputProcessor();
            _renderer = new OutputManagerForTests();

            _engine = new GameEngine(_renderer, _inputProcessor, path);
            _eventManager = _engine.GetEventManager();
            _gameManager = _engine.GetGameManager();
            _stateMachine = _engine.GetGameStateMachine();

            _player = _gameManager.GetPlayer();

            _locationObjectA = new GameObject();
            _locationObjectB = new GameObject();

            _locationA = new LocationComposant("Town", "Main town", _gameManager);
            _locationB = new LocationComposant("Forest", "Dark forest", _gameManager);

            _locationObjectA.AddComposant(_locationA);
            _locationObjectB.AddComposant(_locationB);

            _position = new PositionComposant(_locationObjectA, _renderer);
            _player.AddComposant(_position);
        }

        [Test]
        public void GetCurrentLocationShouldReturnInitialLocation()
        {
            Assert.AreEqual("Town", _position.GetCurrentLocation().GetName());
        }

        [Test]
        public void SetCurrentLocationShouldUpdateLocation()
        {
            _position.SetCurrentLocation(_locationB);

            Assert.AreEqual("Forest", _position.GetCurrentLocation().GetName());
        }

        [Test]
        public void ProcessInputShouldDelegateToCurrentLocation()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A);
            Assert.DoesNotThrow(() => _position.TreatInput(_inputProcessor));
        }

        [Test]
        public void UpdateShouldDelegateToCurrentLocation()
        {
            Assert.DoesNotThrow(() => _position.Update());
        }

        [Test]
        public void FixedUpdateShouldDelegateToCurrentLocation()
        {
            Assert.DoesNotThrow(() => _position.FixedUpdate(5f));
        }

        [Test]
        public void RenderShouldDelegateToCurrentLocation()
        {
            Assert.DoesNotThrow(() => _position.Render());
        }

        [Test]
        public void ChangingLocationAffectsRenderedLocation()
        {
            _position.SetCurrentLocation(_locationB);

            Assert.DoesNotThrow(() => _position.Render());
            Assert.AreEqual("Forest", _position.GetCurrentLocation().GetName());
        }
    }
}
