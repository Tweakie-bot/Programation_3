using System;

namespace Programation_3_DnD_Core
{
    public interface IEntityState
    {
        //
        public void Enter();
        public void Exit();

        //
        public void TreatInput(IInput input_manager);
        public void Update();
        public void Render();
    }
}
