
using System;

namespace Programation_3_DnD_Core
{
    public class DoesNotTradeEntityState : IEntityState
    {
        private IOutput _renderer;

        //
        public DoesNotTradeEntityState(IOutput render)
        {
            _renderer = render;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void TreatInput(IInput input) { }
        public void Update() { }
        public void Render()
        {
            _renderer.WriteLine("Come back later to trade");
        }
    }
}
