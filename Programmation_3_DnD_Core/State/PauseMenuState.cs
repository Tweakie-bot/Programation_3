using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.State
{
    public class PauseMenuState : IState
    {
        private GameStateMachine _gameStateMachine;
        private GameEngine _gameEngine;
        private IOutput _renderer;

        //
        public PauseMenuState(GameStateMachine state_machine, GameEngine game_engine, IOutput renderer) 
        { 
            _gameStateMachine = state_machine;
            _gameEngine = game_engine;
            _renderer = renderer;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(MainMenuState)));
            }
            else if (key == ConsoleKey.P)
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(InGameState)));
            }
            else if (key == ConsoleKey.I)
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(InventoryState)));

            }
        }
        public void Update() { }
        public void FixedUpdate(float dt) { }
        public void Render()
        {
            Panel menu = new Panel(new Markup("[bold yellow]PAUSE MENU[/]\n\n" + "[grey][[ESC]][/] Return to main menu\n" + "[grey][[P]][/] Resume game\n" + "[grey][[I]][/] Open inventory")).Header("[bold]Game Paused[/]").Border(BoxBorder.Double).Padding(2, 1);

            AnsiConsole.Write(Align.Center(menu, VerticalAlignment.Middle));
        }
    }
}
