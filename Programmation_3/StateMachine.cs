using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class StateMachine
    {
        private IState _currentState;
        private IState _mainMenu;
        private IState _inGame;
        private IState _pauseMenu;
        private IState _workingState;
        private IState _inventoryState;
        private IState _merchantState;

        public IState GetInventoryState () => _inventoryState;

        private GameManager _gameManager;

        IOutput _renderer;

        public StateMachine(GameManager manager, IOutput renderer)
        {
            _gameManager = manager;

            _renderer = renderer;

            _mainMenu = new MainMenuState(this, _gameManager, _renderer);
            _currentState = _mainMenu;

            _pauseMenu = new PauseMenuState(this, _gameManager, _renderer);
            _inGame = new InGameState(this, _gameManager, _renderer);
            _workingState = new WorkingState(this, _gameManager, _renderer);
            _inventoryState = new InventoryState(this, _gameManager, _renderer);
            _merchantState = new TradingState(this, _gameManager, _renderer);
        }

        public void ProcessInput(string input)
        {
            _currentState.Input(input);
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void Render()
        {
            _currentState.Render();
        }
        public void SetState(IState state)
        {
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public GameManager GetGameManager()
        {
            return _gameManager;
        }

        public IState GetMainMenuState()
        {
            return _mainMenu;
        }
        public IState GetInGameState()
        {
            return _inGame;
        }

        public IState GetPauseMenuState()
        {
            return _pauseMenu;
        }

        public IState GetWorkingState()
        {
            return _workingState;
        }

        public IState GetTradingState()
        {
            return _merchantState;
        }
    }
}
