using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class CanWorkState : IWorkState
    {
        private WorkStateMachine _machine;

        public CanWorkState(WorkStateMachine machine)
        {
            _machine = machine;
        }

        public void Enter()
        {
            Console.WriteLine("[WorkState]  You can now work!");
        }

        public void Exit()
        {
            Console.WriteLine("[WorkState]  End of working hours.");
        }

        public void FixedUpdate(float deltaTime)
        {
            // Si on sort des horaires de travail, on passe à l’état “cant work”
            if (!_machine.IsWorkingHours())
            {
                _machine.SetState(_machine.GetCantWorkState());
            }
        }
    }
}
