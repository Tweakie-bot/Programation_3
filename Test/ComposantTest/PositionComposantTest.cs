using NUnit.Framework;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using System;

namespace Test.ComposantTest
{
    public class PositionComposantTest
    {
        private IOutput _renderer;
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

            _renderer = new OutputManagerForTests();
            _engine = new GameEngine(_renderer, path);
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

            _position = new PositionComposant(_locationObjectA);
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
            Assert.DoesNotThrow(() => _position.ProcessInput(ConsoleKey.A));
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
