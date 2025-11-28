using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.State;
using Programation_3_DnD.Output;
using System;

namespace Test.StateTest
{
    public class GameStateMachineTest
    {
        private IOutput _renderer;
        private GameEngine _engine;
        private EventManager _eventManager;
        private GameManager _gameManager;
        private GameStateMachine _gameStateMachine;

        [SetUp]
        public void Setup()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

            _renderer = new OutputManagerForTests();
            _engine = new GameEngine(_renderer, path);
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
            Assert.DoesNotThrow(() => { _gameStateMachine.ProcessInput(ConsoleKey.A); });
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
