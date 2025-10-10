using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class InventoryComponent : Component
    {
        List<Item> _inventory = new List<Item>();

        private IOutput _renderer;
        public InventoryComponent(IOutput renderer)
        {
            _renderer = renderer;
        }
        public override void Update()
        {
            //Should do nothing
        }

        public override void Update(int number)
        {
            //Should not be used
        }

        public void Render()
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                _renderer.WriteLine("---------------------------------");
                _renderer.WriteLine((i + 1).ToString());
                _renderer.WriteLine("Name : " + _inventory[i].GetName());
                _renderer.WriteLine("Description : " + _inventory[i].GetDescription());
                _renderer.WriteLine("Amount : "+ _inventory[i].GetCount());
                _renderer.WriteLine("---------------------------------");
            }
        }

        public void Add(Item item)
        {
            _inventory.Add(item);
        }

        public void AddQuantity(string name, int count)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i].GetName() == name)
                {
                    _inventory[i].Add(count);
                }
            }
        }

        public bool Contains(string name)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i].GetName().Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
        public Item GetItem(string name)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i].GetName().Equals(name))
                {
                    return _inventory[i];
                }
            }
            return default;
        }

        public int GetCount()
        {
            return _inventory.Count;
        }
    }
}
