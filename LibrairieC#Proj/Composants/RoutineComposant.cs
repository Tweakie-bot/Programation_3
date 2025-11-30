using System;

namespace Programation_3_DnD_Core
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
        public override void TreatInput(IInput input_manager)
        {
            _entityStateMachine.TreatInput(input_manager);
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
