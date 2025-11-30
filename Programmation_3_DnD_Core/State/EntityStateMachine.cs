
using System;

namespace Programation_3_DnD_Core
{
    public class EntityStateMachine
    {
        private GameEngine _engine;
        private GameObject _linkedGameObject;
        private GameObject _player;
        private IEntityState _entityTradingState;
        private IEntityState _entityWorkingState;
        private IEntityState _DoesNotTrade;
        private IEntityState _DoesotProposeWork;
        private IEntityState _currentTradingState;
        private IEntityState _currentWorkingState;
        private IOutput _render;
        private EventManager _eventManager;
        private GameStateMachine _gameStateMachine;

        private float _totalTime;

        //
        public EntityStateMachine(GameEngine engine, GameObject linked, IOutput render, EventManager event_manager, GameObject player, GameStateMachine state_machine)
        {
            _engine = engine;
            _linkedGameObject = linked;
            _render = render;
            _eventManager = event_manager;
            _player = player;
            _gameStateMachine = state_machine;
        }

        //
        public void EnableTrade()
        {
            _entityTradingState = new ProposeTradeEntityState(_gameStateMachine, _player, _linkedGameObject, _render, _eventManager);
            _DoesNotTrade = new DoesNotTradeEntityState(_render);
            _currentTradingState = _DoesNotTrade;
        }
        public void EnableWork()
        {
            _entityWorkingState = new ProposeWorkEntityState(_engine, _render, _eventManager, _player);
            _DoesotProposeWork = new DoesNotProposeWorkEntityState(_render);
            _currentWorkingState = _DoesotProposeWork;
        }

        //
        public void SetTradeState(IEntityState state)
        {
            _currentTradingState = state;
        }
        public void SetWorkState(IEntityState state)
        {
            _currentWorkingState = state;
        }

        //
        public IEntityState GetCurrentTradeState()
        {
            return _currentTradingState;
        }
        public IEntityState GetCurrentWorkState()
        {
            return _currentWorkingState;
        }

        //
        public void TreatInput(IInput input_manager)
        {
            if (_currentTradingState != null)
            {
                _currentTradingState.TreatInput(input_manager);
            }

            if (_currentWorkingState != null)
            {
                _currentWorkingState.TreatInput(input_manager);
            }
        }
        public void Update()
        {
            if (_currentTradingState != null)
            {
                _currentTradingState.Update();
            }
            if (_currentWorkingState != null)
            {
                _currentWorkingState.Update();
            }
        }
        public void FixedUpdate(float t)
        {
            _totalTime = t;

            if (t == 7)
            {
                _engine.ClearUIMessages();
            }

            if (t > 7f && t < 19f)
            {
                if (_entityTradingState != null)
                {
                    SetTradeState(_entityTradingState);
                }
                if (_entityWorkingState != null)
                {
                    SetWorkState(_entityWorkingState);
                }
            }

            else
            {
                if (_DoesNotTrade != null)
                {
                    SetTradeState(_DoesNotTrade);
                }
                if (_DoesotProposeWork != null)
                {
                    SetWorkState(_DoesotProposeWork);
                }
            }
        }
        public void Render()
        {
            
        }
    }
}
