using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Composants
{
    public class WorkForceComposant : Composant
    {
        //
        private int _workForce = 0;

        //
        public int ApplyWorkForceGain(int gain)
        {
            _workForce += gain;
            return _workForce;
        }

        //
        public int GetWorkForceValue()
        {
            return _workForce;
        }

        //
        public override void ProcessInput(ConsoleKey key) { }
        public override void Update() { }
        public override void FixedUpdate(float time) { }
        public override void Render() { }
    }
}
