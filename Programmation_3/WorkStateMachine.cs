using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class WorkStateMachine
    {
        private IWorkState _currentState;

        private IWorkState _canWorkState;
        private IWorkState _canNotWorkState;

        private float _currentTime;
        private float _timeSpeed = 0.1f;


        public WorkStateMachine()
        {
            _canWorkState = new CanWorkState(this);
            _canNotWorkState = new CanNotWorkState(this);

            _currentState = _canNotWorkState;
        }

        public void FixedUpdate(float deltaTime)
        {
            _currentTime += deltaTime * _timeSpeed;

            if (_currentTime >= 24f)
                _currentTime = 0f;

            _currentState.FixedUpdate(deltaTime);
        }

        public void SetState(IWorkState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public float GetCurrentTime() => _currentTime;
        public bool IsWorkingHours() {
            if (_currentTime >= 8f && _currentTime <= 18f) { return true; }
            return false;
        }

        public IWorkState GetCanWorkState()
        {
            return _canWorkState;
        }
        public IWorkState GetCantWorkState()
        {
            return _canNotWorkState;
        }
    }
}
