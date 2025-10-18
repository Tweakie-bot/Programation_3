using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Composants
{
    internal abstract class Composant
    {
        public virtual void ProcessInput()
        {

        }
        public abstract void Update();
    }
}
