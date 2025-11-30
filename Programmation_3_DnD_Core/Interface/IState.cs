using System;

namespace Programation_3_DnD_Core
{
    public interface IState
    {
        //
        public void Enter();
        public void Exit();

        //
        public void TreatInput(IInput input_manager);
        public void Update();
        public void FixedUpdate(float delta_t);
        public void Render();
    }
}
