using System;

namespace Programation_3_DnD_Core
{
    public class IDComposant : Composant
    {
        // Variable
        private string _name;

        // Constructeur
        public IDComposant(string name)
        {
            _name = name;
        }

        // Getter
        public string GetName()
        {
            return _name;
        }

        // Logique
        public override void TreatInput(IInput input) { }
        public override void Update() { }
        public override void FixedUpdate(float time) { }
        public override void Render() { }
    }
}
