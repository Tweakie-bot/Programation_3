using Programation_3_DnD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.State
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
        public void ProcessInput(ConsoleKey key) { }
        public void Update() { }
        public void Render()
        {
            _renderer.WriteLine("Come back later to trade");
        }
    }
}
