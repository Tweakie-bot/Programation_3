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
    public class RoutineComposantTest
    {
        private IOutput _renderer;
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
            _engine = new GameEngine(_renderer, path);
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
            Assert.DoesNotThrow(() => _routine.ProcessInput(ConsoleKey.T));
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
                _routine.ProcessInput(ConsoleKey.W);
                _routine.FixedUpdate(10f);
                _routine.Update();
                _routine.Render();
            });
        }
    }
}
