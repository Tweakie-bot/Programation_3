using System;

namespace Programation_3_DnD_Core
{
    public class PositionComposant : Composant
    {
        //
        private LocationComposant _currentLocation;

        //
        public PositionComposant(GameObject current_location)
        {
            if (current_location == null)
            {
                throw new ArgumentNullException("Debug in Location");
            }
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
        public override void TreatInput(IInput input_manager)
        {
            _currentLocation.TreatInput(input_manager);
        }
        public override void Update()
        {
            _currentLocation.Update();
        }
        public override void FixedUpdate(float t)
        {
            _currentLocation.FixedUpdate(t);
        }
        public override void Render()
        {
            _currentLocation.Render();
        }
    }
}
