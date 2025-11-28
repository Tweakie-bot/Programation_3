using NUnit.Framework;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using Programation_3_DnD.Output;
using System;

namespace Test.ComposantTest
{
    public class LocationComposantTest
    {
        private IOutput _renderer;
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

            _renderer = new OutputManagerForTests();
            _engine = new GameEngine(_renderer, path);
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
            _locationA.ProcessInput(ConsoleKey.D1);

            PositionComposant position = _player.GetComposant<PositionComposant>();
            Assert.AreEqual("Forest", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void MoveToSecondLocation_WithKey2()
        {
            _locationA.ProcessInput(ConsoleKey.D2);

            PositionComposant position = _player.GetComposant<PositionComposant>();
            Assert.AreEqual("Cave", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void EscapeShouldReturnToPreviousLocation()
        {
            _locationA.ProcessInput(ConsoleKey.D1);
            PositionComposant position = _player.GetComposant<PositionComposant>();

            position.GetCurrentLocation().ProcessInput(ConsoleKey.Escape);

            Assert.AreEqual("Town", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void InvalidKeyDoesNotChangeLocation()
        {
            _locationA.ProcessInput(ConsoleKey.D9);

            PositionComposant position = _player.GetComposant<PositionComposant>();
            Assert.AreEqual("Town", position.GetCurrentLocation().GetName());
        }

        [Test]
        public void CharacterReceivesInput()
        {
            GameObject npc = new GameObject();
            npc.AddComposant(new IDComposant("NPC"));

            _locationA.AddCharacter(npc);

            Assert.DoesNotThrow(() => _locationA.ProcessInput(ConsoleKey.A));
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
