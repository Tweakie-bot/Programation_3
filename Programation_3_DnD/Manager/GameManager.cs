using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;

namespace Programation_3_DnD.Manager
{
    internal class GameManager
    {
        private IOutput _renderer;
        private GameEngine _engine;

        private List<GameObject> _locations;

        private GameObject _player;

        private LocationComposant _currentLocation;

        public void ProcessInput(ConsoleKey key)
        {
            //On donne l'input parce que la liste qui nous intéresse est dans _currentLocation
            _currentLocation.ProcessInput(key);
        }
        public void Update()
        {
            //Update de tout les composants du joueur
            _player.Update();

            _currentLocation.Update();
        }

        public void Render()
        {
            _renderer.WriteLine(_currentLocation.GetName());
            _currentLocation.RenderConnectedLocation();
        }
        public GameManager(IOutput renderer, GameEngine game_engine)
        {
            _renderer = renderer;
            _engine = game_engine;

            _locations = new List<GameObject>();

            //Utile pour quand le jeu commence
            _currentLocation = new LocationComposant("Storm island", "I need to work on a description for storm island", this);

            CreateObjects();
        }

        private void CreateObjects()
        {
            CreatePlayer();
            CreateLocations();
        }

        private void CreateLocations()
        {
            //Création de l'île
            GameObject island = new GameObject();
            island.AddComposant(_currentLocation);
            _locations.Add(island);

            //Création du temple et de ses pièces
            GameObject temple = new GameObject();
            temple.AddComposant(new LocationComposant("Dragon's rest", "I need to work on this", this));
            _locations.Add(temple);

            GameObject temple_01 = new GameObject();
            temple_01.AddComposant(new LocationComposant("Cloister", "Bedroom", this));
            _locations.Add(temple_01);

            //On prend les composants dont on a besoin pour les connecter plus facilement
            LocationComposant island_location = (LocationComposant)island.GetComposant<LocationComposant>();

            LocationComposant temple_location = (LocationComposant)temple.GetComposant<LocationComposant>();
            LocationComposant temple_location_01 = (LocationComposant)temple_01.GetComposant<LocationComposant>();
            temple_01.AddComposant(new TraderComposant());

            //Connexion des composants
            island_location.ConnectToNext(temple_location);

            temple_location.ConnectToNext(temple_location_01);
        }
        private void CreatePlayer()
        {
            _player = new GameObject();
            _player.AddComposant(new PlayerComposant());
        }

        public GameObject GetPlayer() { 
            return _player; 
        }

        public IOutput GetRenderer()
        {
            return _renderer;
        }

        public LocationComposant GetCurrentLocation()
        {
            return _currentLocation;
        }

        public void SetCurrentLocation(LocationComposant location)
        {
            _currentLocation = location;
        }
    }
}
