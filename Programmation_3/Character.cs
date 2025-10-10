using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class Character : GameObject
    {
        private string _name;

        public Character(string name)
        {
            _name = name;
        }
        public string GetName()
        {
            return _name;
        }
    }
}
