using Programation_3_DnD.Composants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Objects
{
    internal class GameObject
    {
        private List<Composant> _composantTable = new List<Composant>();
        public void AddComposant(Composant composant) // Ajout
        {
            _composantTable.Add(composant);
        }

        public bool IsContaining(Composant composant) // Vérifie la disponibilité
        {
            if (_composantTable.Contains(composant))
            {
                return true;
            }

            return false;
        }

        public Composant GetComposant<T>() where T : Composant
        {
            Type type = typeof(T);

            if (_composantTable.Count != 0)
            {
                for (int index = 0; index < _composantTable.Count; index++)
                {
                    if (_composantTable[index].GetType() == type)
                    {
                        return _composantTable[index];
                    }
                }
            }

            throw new Exception("No composant of that type is found");

        }

        public void Update()
        {
            foreach (Composant composant in _composantTable)
            {
                composant.Update();
            }
        }
    }
}
