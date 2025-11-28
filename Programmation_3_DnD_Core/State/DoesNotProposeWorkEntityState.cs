using Programation_3_DnD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.State
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
        public void ProcessInput(ConsoleKey key) { }
        public void Update() { }
        public void FixedUpdate() { }
        public void Render()
        {
            _renderer.WriteLine("Come back later to work");
        }
    }
}
