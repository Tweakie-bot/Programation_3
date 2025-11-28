using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Spectre.Console.Rendering;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.State;
using Programation_3_DnD.Data;
using System.Security.Cryptography.X509Certificates;

namespace Programation_3_DnD.Composants
{
    public class LocationComposant : Composant
    {
        // Variables
        private string _name;
        private string _description;
        private GameManager _gameManager;
        private IOutput _renderer;
        private List<LocationComposant> _connectionTbl = new List<LocationComposant>();
        private LocationComposant _previousLocation;
        private List<GameObject> _characters = new List<GameObject>();

        // Constructeurs
        public LocationComposant(string name, string description, GameManager manager)
        {
            _name = name;
            _description = description;

            _gameManager = manager;

            _renderer = _gameManager.GetRenderer();
        }
        public LocationComposant(LocationData data, GameManager manager)
        {
            _name = data.GetName();
            _description = data.GetDescription();

            _gameManager = manager;
            _renderer = _gameManager.GetRenderer();
        }

        // Méthodes
        public void AddCharacter(GameObject o)
        {
            _characters.Add(o);
        }
        public void ConnectToNext(LocationComposant location)
        {
            if (!_connectionTbl.Contains(location))
            {
                _connectionTbl.Add(location);
                location.SetPreviousLocation(this);
            }
        }

        // Setter
        public void SetPreviousLocation(LocationComposant previous)
        {
            _previousLocation = previous;
        }

        // Getters
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
            return _connectionTbl.Count();
        }
        public LocationComposant GetPreviousLocation()
        {
            return _previousLocation;
        }
        public LocationComposant GetLocationAtIndex(int index)
        {
            return _connectionTbl[index];
        }
        public int GetCharactersCount()
        {
            return _characters.Count();
        }
        public List<GameObject> GetCopyOfCharacterTable()
        {
            List<GameObject> copy = new List<GameObject>(_characters);

            return copy;
        }
        // Logique
        public override void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape && _previousLocation != null)
            {
                _gameManager.GetPlayer().SetCurrentLocation(_previousLocation);
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
                    _gameManager.GetPlayer().SetCurrentLocation(_connectionTbl[input - 1]);
                }
            }

            if (_characters.Count > 0)
            {
                for (int i = 0; i < _characters.Count; i++)
                {
                    _characters[i].ProcessInput(key);
                }
            }
        }
        public override void Update()
        {
            if (_characters.Count > 0)
            {
                for (int i = 0; i < _characters.Count; i++)
                {
                    _characters[i].Update();
                }
            }
        }
        public override void FixedUpdate(float t)
        {
            if (_characters.Count > 0)
            {
                for (int i = 0; i < _characters.Count; i++)
                {
                    _characters[i].FixedUpdate(t);
                }
            }
        }
        public override void Render()
        {
            _renderer.SetLocation(this);
        }
    }
}
