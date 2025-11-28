using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Composants
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
        public override void ProcessInput(ConsoleKey key) { }
        public override void Update() { }
        public override void FixedUpdate(float time) { }
        public override void Render() { }
    }
}
