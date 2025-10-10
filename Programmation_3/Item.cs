using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class Item : GameObject
    {
        private string _name;
        private string _description;
        private int _count;

        public Item(string name, string description, int count)
        {
            _name = name;
            _description = description;
            _count = count;
        }

        public void Add(int add)
        {
            _count += add;
        }

        public string GetName()
        {
            return _name;
        }
        public string GetDescription()
        {
            return _description;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
