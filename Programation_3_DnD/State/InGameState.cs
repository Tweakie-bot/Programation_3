using Programation_3_DnD.Manager;
using Programation_3_DnD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Objects;
using Spectre.Console;
using Programation_3_DnD.Composants;
using Spectre.Console.Rendering;

namespace Programation_3_DnD.State
{
    public class InGameState : IState
    {
        private GameEngine _gameEngine;
        private IOutput _renderer;
        GameObject _player;
        GameStateMachine _gameStateMachine;
        private float _totalTime;

        //
        public InGameState(GameEngine game_engine, GameStateMachine state_machine, IOutput renderer, GameObject player)
        {
            _gameEngine = game_engine;
            _renderer = renderer;
            _gameStateMachine = state_machine;
            _player = player;
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.P) 
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(PauseMenuState)));
            }
        }
        public void Update() { }
        public void FixedUpdate(float d_t)
        {
            _totalTime = d_t;
        }

        
        public void Render()
        {
            PositionComposant position = _player.GetComposant<PositionComposant>();

            Panel header = new Panel(new Markup($"[bold]STATE :[/] [yellow]IN GAME[/]\n[grey]TIME[/] {_totalTime:0.0}\n\n[[P]] Pause Menu")).Header("[bold]Status[/]").Border(BoxBorder.Rounded);

            IRenderable game_view = position.GetCurrentLocation().RenderLocationPanel();

            Layout layout = new Layout().SplitColumns(new Layout("left").Ratio(1), new Layout("right").Ratio(2));

            layout["left"].Update(header);

            IReadOnlyList<string> messages = _gameEngine.GetUIMessages();

            if (messages.Count > 0)
            {
                Grid message_grid = new Grid();
                message_grid.AddColumn();

                foreach (string message_grid_i in messages)
                {
                    message_grid.AddRow(new Markup($"[green]{message_grid_i}[/]"));
                }

                Panel eventPanel = new Panel(message_grid).Header("[bold]Events[/]").Border(BoxBorder.Rounded);

                layout["right"].Update(new Rows(game_view,eventPanel));
            }
            else
            {
                layout["right"].Update(game_view);
            }

            AnsiConsole.Write(layout);
        }

    }
}
