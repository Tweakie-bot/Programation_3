using System;

namespace Programation_3_DnD_Core
{
    public abstract class Composant
    {
        // Logique
        public abstract void TreatInput(IInput input_manager);
        public abstract void Update();
        public abstract void FixedUpdate(float t);
        public abstract void Render();
    }
}
