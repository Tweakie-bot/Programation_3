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
        public IRenderable RenderLocationPanel()
        {
            Grid grid = new Grid();
            grid.AddColumn();
            grid.AddColumn();

            grid.AddRow(new Markup($"[bold yellow]{_name}[/]"), new Markup($"[grey]{_description}[/]"));

            if (_connectionTbl.Count > 0)
            {
                for (int i = 0; i < _connectionTbl.Count; i++)
                {
                    LocationComposant next_destination = _connectionTbl[i];
                    grid.AddRow(new Markup($"[green]{i + 1}[/]"), new Markup(next_destination.GetName()));
                }
            }

            if (_previousLocation != null)
            {
                grid.AddRow(new Markup("[red]ESC[/]"), new Markup($"Go back to : {_previousLocation.GetName()}"));
            }

            if (_characters.Count > 0)
            {
                foreach (GameObject character in _characters)
                {
                    RoutineComposant routine = character.GetComposant<RoutineComposant>();

                    EntityStateMachine entity_state_machine = routine.GetEntityStateMachine();

                    string message = "";

                    if (entity_state_machine.GetCurrentTradeState() is ProposeTradeEntityState)
                    {
                        message = "Press T to trade";
                    }
                    else if (entity_state_machine.GetCurrentTradeState() is DoesNotTradeEntityState) 
                    {
                        message = "Come back later to trade";
                    }

                    if (entity_state_machine.GetCurrentWorkState() is ProposeWorkEntityState)
                    {
                        message += (message != "" ? "\n" : "") + "Press W to work";
                    }
                    else if (entity_state_machine.GetCurrentWorkState() is DoesNotProposeWorkEntityState)
                    {
                        message += (message != "" ? "\n" : "") + "Come back later to work";
                    }

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        grid.AddRow(new Markup($"[italic]{message}[/]"), new Text(""));
                    }
                }
            }
            return new Panel(grid).Header("[bold]Location[/]").Border(BoxBorder.Rounded).Padding(1, 1, 1, 1);
        }
        public override void Render()
        {
            /*
            _renderer.WriteLine(_name);

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

            if (_characters.Count > 0)
            {
                for (int i = 0; i < _characters.Count; i++)
                {
                    _characters[i].Render();
                }
            }
            */
        }
    }
}
