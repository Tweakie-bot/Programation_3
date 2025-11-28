using Programation_3_DnD.Composants;
using Programation_3_DnD.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Objects
{
    public class GameObject
    {
        //
        private List<Composant> _composantTable = new List<Composant>();

        //
        public void AddComposant(Composant composant)
        {
            _composantTable.Add(composant);
        }

        //
        public void SetCurrentLocation(LocationComposant location)
        {
            for (int i = 0; i < _composantTable.Count; i++)
            {
                if (_composantTable[i] as PositionComposant != null)
                {
                    (_composantTable[i] as PositionComposant).SetCurrentLocation(location);
                }
            }
        }

        //
        public bool IsContaining(Composant composant)
        {
            if (_composantTable.Contains(composant))
            {
                return true;
            }

            return false;
        }
        public T GetComposant<T>() where T : Composant
        {
            Type type = typeof(T);

            if (_composantTable.Count != 0)
            {
                for (int index = 0; index < _composantTable.Count; index++)
                {
                    if (_composantTable[index].GetType() == type)
                    {
                        return _composantTable[index] as T;
                    }
                }
            }
            throw new Exception("No composant of that type is found");
        }

        //
        public void ProcessInput(ConsoleKey key)
        {
           foreach(Composant comp in _composantTable)
            {
                comp.ProcessInput(key);
            }
        }
        public void Update()
        {
            foreach (Composant comp in _composantTable)
            {
                comp.Update();
            }
        }
        public void FixedUpdate(float t)
        {
            foreach (Composant comp in _composantTable)
            {
                comp.FixedUpdate(t);
            }
        }
        public void Render()
        {
            foreach (Composant comp in _composantTable)
            {
               comp.Render();
            }
        }
    }
}
