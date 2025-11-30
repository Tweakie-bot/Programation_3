using System;
using System.Collections.Generic;
using System.Linq;

namespace Programation_3_DnD_Core
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

        public List<LocationComposant> GetListOfLocation()
        {
            return new List<LocationComposant>(_connectionTbl);
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
        public override void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyCancel() && _previousLocation != null)
            {
                _gameManager.GetPlayer().SetCurrentLocation(_previousLocation);
                return;
            }

            int input = input_manager.GetNumberPressed();

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
                    _characters[i].TreatInput(input_manager);
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
