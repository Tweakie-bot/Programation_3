
using System;

namespace Programation_3_DnD_Core
{
    public class DoesNotProposeWorkEntityState : IEntityState
    {
        private IOutput _renderer;

        //
        public DoesNotProposeWorkEntityState(IOutput renderer)
        {
            _renderer = renderer;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void TreatInput(IInput input) { }
        public void Update() { }
        public void FixedUpdate() { }
        public void Render()
        {
            _renderer.WriteLine("Come back later to work");
        }
    }
}
