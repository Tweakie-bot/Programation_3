using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.State;
using Programation_3_DnD.Event;
using System;

namespace Test.StateTest
{
    public class MainMenuStateTest
    {
        private IOutput _renderer;
        private GameEngine _engine;
        private EventManager _eventManager;
        private GameManager _gameManager;
        private GameStateMachine _gameStateMachine;
        private IState _state;

        [SetUp]
        public void Setup()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

            _renderer = new OutputManagerForTests();
            _engine = new GameEngine(_renderer, path);
            _eventManager = _engine.GetEventManager();
            _gameManager = _engine.GetGameManager();
            _gameStateMachine = _engine.GetGameStateMachine();

            _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(MainMenuState)));
            _state = _gameStateMachine.GetCurrentState();
        }

        [Test]
        public void TryEnterKeyGoToInGameState()
        {
            _state.ProcessInput(ConsoleKey.Enter);

            Assert.IsInstanceOf<InGameState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryRandomKeyNoCrash()
        {
            _state.ProcessInput(ConsoleKey.A);
            Assert.Pass();
        }

        [Test]
        public void TryRenderNoCrash()
        {
            _state.Render();
            Assert.Pass();
        }
    }
}

