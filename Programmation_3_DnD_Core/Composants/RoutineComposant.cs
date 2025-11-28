using Programation_3_DnD.Interface;
using Programation_3_DnD.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Composants
{
    public class RoutineComposant : Composant
    {
        //
        EntityStateMachine _entityStateMachine;

        //
        public RoutineComposant (EntityStateMachine entity_state_machine)
        {
            _entityStateMachine = entity_state_machine;
        }

        //
        public EntityStateMachine GetEntityStateMachine()
        {
            return _entityStateMachine;
        }

        //
        public override void ProcessInput(ConsoleKey key)
        {
            _entityStateMachine.ProcessInput(key);
        }
        public override void FixedUpdate(float t)
        {
            _entityStateMachine.FixedUpdate(t);
        }
        public override void Update()
        {
            _entityStateMachine.Update();
        }
        public override void Render()
        {
            _entityStateMachine.Render();
        }
    }
}
