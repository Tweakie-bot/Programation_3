using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class Pnj : Character
    {
        public Pnj(string name, LocationComponent locationComponent) : base(name)
        {
            AddComponent(locationComponent);
        }
    }
}
