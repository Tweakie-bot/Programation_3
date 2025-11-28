using Programation_3_DnD.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Composants
{
    public class PositionComposant : Composant
    {
        //
        private LocationComposant _currentLocation;

        //
        public PositionComposant(GameObject current_location)
        {
            _currentLocation = current_location.GetComposant<LocationComposant>();
        }

        //
        public void SetCurrentLocation(LocationComposant location)
        {
            _currentLocation = location;
        }

        //
        public LocationComposant GetCurrentLocation()
        {
            return _currentLocation;
        }

        //
        public override void ProcessInput(ConsoleKey key)
        {
            _currentLocation.ProcessInput(key);
        }
        public override void Update()
        {
            _currentLocation.Update();
        }
        public override void FixedUpdate(float t)
        {
            _currentLocation?.FixedUpdate(t);
        }
        public override void Render()
        {
            _currentLocation.Render();
        }
    }
}
