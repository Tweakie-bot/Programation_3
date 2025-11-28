using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Composants
{
    public abstract class Composant
    {
        // Logique
        public abstract void ProcessInput(ConsoleKey key);
        public abstract void Update();
        public abstract void FixedUpdate(float t);
        public abstract void Render();
    }
}
