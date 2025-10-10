using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class Player : Character
    {
        public Player(string name, LocationComponent locationComponent) : base(name)
        {
            AddComponent(locationComponent);
        }
    }
}
