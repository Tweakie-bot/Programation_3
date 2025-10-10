using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public abstract class Component
    {
        public abstract void Update();

        public abstract void Update(int number);
    }
}
