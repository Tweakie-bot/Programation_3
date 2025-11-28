using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using Programation_3_DnD.Data;
using System.IO;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Programation_3_DnD.Manager
{
    public class GameManager
    {
        //
        private IOutput _renderer;
        private GameEngine _gameEngine;
        private GameObject _player = new GameObject();
        private EventManager _eventManager;
        private GameStateMachine _gameStateMachine;

        private Dictionary<string, ItemComposant> _items;
        private Dictionary<string, GameObject> _characters;
        private Dictionary<string, GameObject> _locations;

        private string _dataPath;

        //
        public GameManager(IOutput renderer, GameEngine game_engine, EventManager event_manager, string json_path)
        {
            _renderer = renderer;
            _gameEngine = game_engine;
            _eventManager = event_manager;

            _locations = new Dictionary<string, GameObject>();
            _characters = new Dictionary<string, GameObject>();
            _items = new Dictionary<string, ItemComposant>();

            _dataPath = json_path;
        }

        //
        private void CreateObjects()
        {
            CreateItems();
            CreateCharacters();
            CreatePlayer(CreateLocations());
        }
        private void CreateItems()
        {
            string items_folder = Path.Combine(_dataPath, "Items");
            List<ItemData> simple_items = DataManager.LoadAll<ItemData>(items_folder);

            for (int i = 0; i < simple_items.Count; i++)
            {
                ItemData item = simple_items[i];

                ItemComposant item_composant = new ItemComposant(item.GetName(), item.GetValueInGold());

                _items.Add(item.GetName(), item_composant);
            }

            string weapons_folder = Path.Combine(_dataPath, "Weapons");
            List<WeaponData> weapon_items = DataManager.LoadAll<WeaponData>(weapons_folder);

            for (int i = 0; i < weapon_items.Count; i++)
            {
                WeaponData data = weapon_items[i];

                WeaponComposant weapon = new WeaponComposant(data.GetName(), data.GetValueInGold(), data.GetDamage());

                _items.Add(data.GetName(), weapon);
            }
        }
        private GameObject CreateLocations()
        {
            string locations_path = Path.Combine(_dataPath, "Locations");
            List<LocationData> data_list = DataManager.LoadAll<LocationData>(locations_path);

            for (int i = 0; i < data_list.Count; i++)
            {
                LocationData data = data_list[i];

                GameObject location_obj = new GameObject();
                LocationComposant location_comp = new LocationComposant(data, this);

                location_obj.AddComposant(location_comp);

                _locations.Add(data.GetName(), location_obj);
            }

            for (int i = 0; i < data_list.Count; i++)
            {
                LocationData data = data_list[i];

                LocationComposant current_location = _locations[data.GetName()].GetComposant<LocationComposant>();

                if (!data.GetNextLocationNull())
                {
                    for (int j = 0; j < data.GetNextLocationsCount(); j++)
                    {
                        string next_name = data.GetLocationAt(j);

                        if (_locations.ContainsKey(next_name))
                        {
                            LocationComposant next_comp = _locations[next_name].GetComposant<LocationComposant>();

                            current_location.ConnectToNext(next_comp);
                        }
                        else
                        {
                            _renderer.WriteLine("WARNING: Next location does not exist -> " + next_name);
                        }
                    }
                }

                if (!data.GetCharactersNull())
                {
                    for (int j = 0; j < data.GetCharactersCount(); j++)
                    {
                        string char_name = data.GetCharacterAt(j);

                        if (_characters.ContainsKey(char_name))
                        {
                            GameObject character = _characters[char_name];
                            current_location.AddCharacter(character);
                        }
                        else
                        {
                            _renderer.WriteLine("WARNING: Character not found -> " + char_name);
                        }
                    }
                }
            }

            return _locations["Storm Island"];
        }
        private void CreatePlayer(GameObject location)
        {
            string file = Path.Combine(_dataPath, "Player", "Player.json");

            PlayerData data = DataManager.Load<PlayerData>(file);

            _player.AddComposant(new PositionComposant(location));
            _player.AddComposant(new IDComposant(data.GetName()));
            _player.AddComposant(new WorkForceComposant());

            InventoryComposant inventory = new InventoryComposant(_renderer);
            _player.AddComposant(inventory);

            for (int i = 0; i < data.GetInventoryCount(); i++)
            {
                string item_name = data.GetItemAt(i).GetItemName();
                int count = data.GetItemAt(i).GetCount();

                if (_items.ContainsKey(item_name))
                {
                    inventory.Add(_items[item_name], count);
                }
                else
                {
                    _renderer.WriteLine("Unknown item in Player.json : " + item_name);
                }
            }
        }
        private void CreateCharacters()
        {
            string characters_path = Path.Combine(_dataPath, "Npcs");

            if (!Directory.Exists(characters_path))
            {
                _renderer.WriteLine("No NPC folder found at: " + characters_path);
                return;
            }

            List<CharacterData> character_files = DataManager.LoadAll<CharacterData>(characters_path);

            for (int i = 0; i < character_files.Count; i++)
            {
                CharacterData data = character_files[i];

                GameObject npc = new GameObject();

                npc.AddComposant(new IDComposant(data.GetName()));

                InventoryComposant inventory = new InventoryComposant(_renderer);
                npc.AddComposant(inventory);

                for (int j = 0; j < data.GetInventoryCount(); j++)
                {
                    string item_name = data.GetItemAt(j).GetItemName();
                    int count = data.GetItemAt(j).GetCount();

                    if (_items.ContainsKey(item_name))
                    {
                        inventory.Add(_items[item_name], count);
                    }
                    else
                    {
                        _renderer.WriteLine("Unknown item for " + data.GetName() + ": " + item_name);
                    }
                }

                EntityStateMachine state = new EntityStateMachine(_gameEngine, npc, _renderer, _eventManager, _player, _gameStateMachine);

                if (data.GetTrade()) state.EnableTrade();
                if (data.GetWork()) state.EnableWork();

                npc.AddComposant(new RoutineComposant(state));

                _characters.Add(data.GetName(), npc);
            }
        }
        public void AddStateMachine(GameStateMachine state_machine)
        {
            _gameStateMachine = state_machine;

            CreateObjects();
        }

        //
        public GameObject GetPlayer()
        {
            return _player;
        }
        public IOutput GetRenderer()
        {
            return _renderer;
        }

        //
        public void ProcessInput(ConsoleKey key)
        {
            _player.ProcessInput(key);
        }
        public void Update()
        {
            _player.Update();
        }
        public void FixedUpdate(float t)
        {
            _player.FixedUpdate(t);
        }
        public void Render()
        {
            _player.Render();
        }
    }
}
