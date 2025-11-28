using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.State
{
    public class InventoryState : IState
    {
        GameStateMachine _gameStateMachine;
        private GameManager _gameManager;
        private GameObject _playerGameObject;

        //
        public InventoryState(GameEngine engine, GameStateMachine machine,GameManager manager, IOutput render)
        {
            _gameStateMachine = machine;
            _gameManager = manager;
            _playerGameObject = _gameManager.GetPlayer();
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Q)
            {
                _gameStateMachine.SetState(_gameStateMachine.GetState(typeof(PauseMenuState)));
            }
        }
        public void Update() { }
        public void FixedUpdate(float t) { }
        public void Render()
        {
            InventoryComposant inventory = _playerGameObject.GetComposant<InventoryComposant>();

            Panel header = new Panel(new Markup("[bold yellow]INVENTORY[/] \n \n [grey][[Q]][/] Retour")).Header("[bold]Menu[/]").Border(BoxBorder.Rounded);

            int gold = inventory.GetCount("Gold");

            Panel gold_panel = new Panel(new Markup($"[yellow]Gold[/] \n [bold]{gold}[/]")).Header("[bold]$ [/]").Border(BoxBorder.Rounded);

            IRenderable inventory_panel = inventory.RenderInventoryPanel(exclude_gold: true);

            Layout right_layout = new Layout().SplitRows(new Layout("gold").Size(10),new Layout("items"));

            right_layout["gold"].Update(gold_panel);
            right_layout["items"].Update(inventory_panel);

            Layout layout = new Layout().SplitColumns(new Layout("left").Ratio(1), new Layout("right").Ratio(2));

            layout["left"].Update(header);
            layout["right"].Update(right_layout);

            AnsiConsole.Write(layout);
        }

    }
}
