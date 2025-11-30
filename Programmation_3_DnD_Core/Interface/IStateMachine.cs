using System;

namespace Programation_3_DnD_Core
{
    public interface IStateMachine
    {
        public void TreatInput(IInput input_manager);
        public void Update();
        public void FixedUpdate(float delta);
        public void Render();
    }
}
