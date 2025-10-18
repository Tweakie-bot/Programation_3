using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Composants
{
    internal class LocationComposant : Composant
    {
        private string _name;

        private string _description;

        private GameManager _gameManager;

        private IOutput _renderer;

        private List<LocationComposant> _connectionTbl = new List<LocationComposant>();

        private LocationComposant _previousLocation;

        private List<Composant> _composant;
        public LocationComposant(string name, string description, GameManager manager)
        {
            _name = name;
            _description = description;

            _gameManager = manager;

            _renderer = _gameManager.GetRenderer();
        }
        public string GetName()
        {
            return _name;
        }
        public string GetDescription()
        {
            return _description;
        }

        public void ConnectToNext(LocationComposant location)
        {
            if (!_connectionTbl.Contains(location))
            {
                _connectionTbl.Add(location);
                location.SetPreviousLocation(this);
            }

            // Permet à Storm island de ne pas avoir d'option retour arrière
        }

        public void SetPreviousLocation(LocationComposant previous)
        {
            _previousLocation = previous;
        }

        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape && _previousLocation != null)
            {
                _gameManager.SetCurrentLocation(_previousLocation);
                return;
            }

            int input = 0;

            if (key >= ConsoleKey.D1 && key <= ConsoleKey.D9)
            {
                input = (int)(key - ConsoleKey.D0);
            }
            else if (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad9)
            {
                input = (int)(key - ConsoleKey.NumPad0);
            }

            if (input != 0)
            {
                if (input < _connectionTbl.Count + 1)
                {
                    _gameManager.SetCurrentLocation(_connectionTbl[input - 1]);
                }
            }

            foreach (Composant composant in _composant)
            {
                composant.ProcessInput();
            }
        }

        public override void Update()
        {
            foreach (Composant composant in _composant)
            {
                composant.Update();
            }
        }

        public void RenderConnectedLocation()
        {
            if (_connectionTbl.Count > 0)
            {
                for (int i = 0; i < _connectionTbl.Count; i++)
                {
                    _renderer.WriteLine($"{i + 1}.  {_connectionTbl[i].GetName()}");
                }
            }

            if (_previousLocation != null)
            {
                _renderer.WriteLine($"[ESCAPE] to go back to {_previousLocation.GetName()}");
            }
        }

        public void AddComposant(Composant composant) // Ajout
        {
            _composant.Add(composant);
        }

        public bool IsContaining(Composant composant) // Vérifie la disponibilité
        {
            if (_composant.Contains(composant))
            {
                return true;
            }

            return false;
        }

        public Composant GetComposant<T>() where T : Composant
        {
            Type type = typeof(T);

            if (_composant.Count != 0)
            {
                for (int index = 0; index < _composant.Count; index++)
                {
                    if (_composant[index].GetType() == type)
                    {
                        return _composant[index];
                    }
                }
            }

            throw new Exception("No composant of that type is found");

        }
    }
}
