using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class Location : GameObject
    {
        private string _name;
        private string _description;

        private bool _canWork;
        private bool _canTrade;

        private IOutput _renderer;

        private List<Location> _connectedLocations;

        public Location(string name, string description, IOutput renderer)
        {
            _name = name;
            _description = description;

            _connectedLocations = new List<Location>();
            _renderer = renderer;
        }

        public void ConnectALocation(Location location)
        {
            if (! _connectedLocations.Contains(location))
            {
                _connectedLocations.Add(location);
            }

            if (!location.GetLocationList().Contains(this))
            {
                location.ConnectALocation(this);
            }
        }

        public void SetCanWork(bool boolean)
        {
            _canWork = boolean;
        }

        public void SetCanTrade(bool boolean)
        {
            _canTrade = boolean;
        }

        public bool GetCanTrade()
        {
            return _canTrade;
        }
        public bool CanWork()
        {
            return _canWork;
        }

        public void Enter()
        {
            _renderer.WriteLine("You enter : " + _name);
        }

        public void Exit()
        {
            _renderer.WriteLine("You exit : " + _name);
        }

        public void Render()
        {
            _renderer.WriteLine(_description);
            _renderer.WriteLine(" ");
            _renderer.WriteLine("$ Use your number pad to choose a target destination");
            _renderer.WriteLine(" ");
            DisplayConnection();
        }

        public Location UpdateLocation(int input)
        {
            try
            {
                return _connectedLocations[input - 1];
            } 
            
            catch
            {
                return this;
            }
        }

        public void DisplayConnection()
        {
            for (int index = 0; index < _connectedLocations.Count(); index++)
            {
                _renderer.WriteLine($"{index + 1} .   {_connectedLocations[index]._name}");
                _renderer.WriteLine(" ");
            }
        }

        public List<Location> GetLocationList()
        {
            return _connectedLocations;
        }
    }
}
