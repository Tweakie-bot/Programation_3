using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public abstract class GameObject
    {
        private List<Component> _components;

        protected GameObject()
        {
            _components = new List<Component>();
        }

        public void AddComponent(Component component)
        {
            Type type_1 = component.GetType();
            for (int index = 0; index < _components.Count(); index++)
            {
                if (_components[index].GetType() == type_1)
                {
                    throw new Exception("Don't give a Game Object the same component twice");
                }
            }
            _components.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < _components.Count(); i++)
            {
                if (_components[i] as T != null)
                {
                    return (T) _components[i];
                }
            }
            return null;
        }

        public void Update(int number)
        {
            for (int i = 0; i < _components.Count(); i++)
            {
                if (_components[i] is LocationComponent)
                {
                    _components[i].Update(number);
                }
            }
        }
    }
}
