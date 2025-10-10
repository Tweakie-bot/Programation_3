using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class LocationComponent : Component
    {
        private Location _currentLocation;

        public LocationComponent(Location location)
        {
            _currentLocation = location;
        }
        public void SetLocation(Location location)
        {
            _currentLocation = location;
        }

        public Location GetLocation()
        {
            return _currentLocation;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
        public override void Update(int input)
        {
            if (_currentLocation != null)
            {
                _currentLocation = _currentLocation.UpdateLocation(input);
            }
        }

        public void Render()
        {
            _currentLocation?.Render();
        }
    }
}
