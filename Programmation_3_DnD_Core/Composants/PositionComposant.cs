using System;

namespace Programation_3_DnD_Core
{
    public class PositionComposant : Composant
    {
        //
        private LocationComposant _currentLocation;
        private IOutput _renderer;

        //
        public PositionComposant(GameObject current_location, IOutput renderer)
        {
            if (current_location == null)
            {
                throw new ArgumentNullException("Debug in Location");
            }
            _currentLocation = current_location.GetComposant<LocationComposant>();
            _renderer = renderer;

            _renderer.SetCurrentLocation(_currentLocation);
            _renderer.SetListOfLocations(_currentLocation.GetListOfLocation());
            _renderer.SetPreviousLocation(_currentLocation.GetPreviousLocation());
        }

        //
        public void SetCurrentLocation(LocationComposant location)
        {
            _currentLocation = location;

            _renderer.SetCurrentLocation(_currentLocation);
            _renderer.SetListOfLocations(_currentLocation.GetListOfLocation());
            _renderer.SetPreviousLocation(_currentLocation.GetPreviousLocation());
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
