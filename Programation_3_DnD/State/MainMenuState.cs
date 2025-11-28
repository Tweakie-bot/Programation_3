using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Spectre.Console;

namespace Programation_3_DnD.State
{
    public class MainMenuState : IState
    {
        private GameEngine _gameEngine;
        private GameStateMachine _gameStateMachine;
        private IOutput _renderer;
        private EventManager _eventManager;

        //
        public MainMenuState(GameEngine game_engine, GameStateMachine state_machine, IOutput renderer, EventManager event_manager)
        {
            _gameEngine = game_engine;
            _gameStateMachine = state_machine;
            _renderer = renderer;
            _eventManager = event_manager;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(InGameState)));
            }
            if (key == ConsoleKey.Escape)
            {
                _eventManager.RegisterEvent(new QuitGameEvent(_gameEngine));
            }
        }
        public void Update() { }
        public void FixedUpdate(float delta) { }
        public void Render()
        {
            Panel menu = new Panel(new Markup("[bold yellow]THE DRAGONS OF STORMWRECK ISLAND[/]\n\n" + "[green][[ENTER]][/]\tPlay\n" + "[red][[ESC]][/]\tQuit"))
            .Header("[bold underline]MAIN MENU[/]")
            .BorderColor(Color.Grey)
            .Padding(2, 1);

            AnsiConsole.Write(Align.Center(menu));
        }
    }
}
