using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using Programation_3_DnD.Event;
using Programation_3_DnD.Output;
using System;

namespace Test.StateTest
{
    public class PauseMenuStateTest
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

            _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(PauseMenuState)));
            _state = _gameStateMachine.GetCurrentState();
        }

        [Test]
        public void TryEscapeGoToMainMenu()
        {
            _state.ProcessInput(ConsoleKey.Escape);

            Assert.IsInstanceOf<MainMenuState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryPressPGoToInGame()
        {
            _state.ProcessInput(ConsoleKey.P);

            Assert.IsInstanceOf<InGameState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryPressIGoToInventory()
        {
            _state.ProcessInput(ConsoleKey.I);

            Assert.IsInstanceOf<InventoryState>(_gameStateMachine.GetCurrentState());
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

