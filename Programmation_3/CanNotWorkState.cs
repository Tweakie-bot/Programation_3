using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class CanNotWorkState : IWorkState
    {
        private WorkStateMachine _machine;

        public CanNotWorkState(WorkStateMachine machine)
        {
            _machine = machine;
        }

        public void Enter()
        {
            Console.WriteLine("[WorkState]  You can't work right now.");
        }

        public void Exit()
        {
            Console.WriteLine("[WorkState]  Working hours have started!");
        }

        public void FixedUpdate(float deltaTime)
        {
            if (_machine.IsWorkingHours())
            {
                _machine.SetState(_machine.GetCanWorkState());
            }
        }
    }
}
