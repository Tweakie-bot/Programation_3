using System;

namespace Programation_3_DnD_Core
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
        public override void TreatInput(IInput input_manager) { }
        public override void Update() { }
        public override void FixedUpdate(float time) { }
        public override void Render() { }
    }
}
