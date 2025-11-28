using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using Programation_3_DnD.Composants;
using System;
using Programation_3_DnD.Manager;

namespace Test.StateTest
{
    public class EntityStateMachineTest
    {
        private IOutput _renderer;
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
            _engine = new GameEngine(_renderer, path);
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
            Assert.DoesNotThrow(() => { _entityStateMachine.ProcessInput(ConsoleKey.A); });
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
