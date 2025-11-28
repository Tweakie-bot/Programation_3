using Programation_3_DnD.Composants;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using Spectre.Console;
using System;

namespace Programation_3_DnD.State
{
    public class TradingState : IState
    {
        private GameStateMachine _machine;
        private IOutput _renderer;
        private GameObject _player;
        private GameObject _merchant;
        private InventoryComposant _playerInv;
        private InventoryComposant _merchantInv;

        private int _selected;

        //
        public TradingState(GameStateMachine machine, IOutput renderer,
                            GameObject player, GameObject merchant)
        {
            _machine = machine;
            _renderer = renderer;
            _player = player;
            _merchant = merchant;

            _playerInv = _player.GetComposant<InventoryComposant>();
            _merchantInv = _merchant.GetComposant<InventoryComposant>();

            _selected = 0;
        }

        //
        public void Enter() { }

        public void Exit() { }

        //
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow)
            {
                _selected--;
                if (_selected < 0) _selected = 2;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                _selected++;
                if (_selected > 2) _selected = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                if (_selected == 0)
                {
                    _machine.SetState(
                        new BuyingState(_machine, _renderer, _player, _merchant)
                    );
                }
                else if (_selected == 1)
                {
                    _machine.SetState(
                        new SellingState(_machine, _renderer, _player, _merchant)
                    );
                }
                else if (_selected == 2)
                {
                    _machine.SetState(_machine.GetState(typeof(InGameState)));
                }
            }
        }
        public void Update() { }
        public void FixedUpdate(float delta) { }
        public void Render()
        {
            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();

            int player_gold = player_inventory.GetCount("Gold");
            int merchant_gold = merchant_inventory.GetCount("Gold");

            string[] options = { "Buy", "Sell", "Quit" };

            Grid main = new Grid();
            main.AddColumn();
            main.AddColumn();

            Grid gold_grid = new Grid();
            gold_grid.AddColumn();
            gold_grid.AddColumn();

            gold_grid.AddRow(new Markup($"[yellow]Player Gold[/]"), new Markup($"[yellow]Merchant Gold[/]"));

            gold_grid.AddRow(new Markup($"[bold]{player_gold}[/]"), new Markup($"[bold]{merchant_gold}[/]"));

            Panel gold_panel = new Panel(gold_grid).Header("[bold]Gold[/]").Border(BoxBorder.Rounded);

            Grid menu_grid = new Grid();
            menu_grid.AddColumn();

            for (int i = 0; i < options.Length; i++)
            {
                if (i == _selected) // ton index actuel
                {
                    menu_grid.AddRow(new Markup($"[black on yellow]> {options[i]} <[/]"));
                }
                else
                {
                    menu_grid.AddRow(new Markup($"  {options[i]}"));
                }
            }

            Panel menu_panel = new Panel(menu_grid).Header("[bold]Trading[/]").Border(BoxBorder.Double);

            main.AddRow(gold_panel, menu_panel);

            AnsiConsole.Write(main);
        }


        /*
        public void Render()
        {
            _renderer.WriteLine("===== Trading =====");
            _renderer.WriteLine("Your gold : " + _playerInv.GetCount("Gold"));
            _renderer.WriteLine("Merchant's gold : " + _merchantInv.GetCount("Gold"));
            _renderer.PassLine();

            string[] entries = new string[3];
            entries[0] = "Buy";
            entries[1] = "Sell";
            entries[2] = "Quit trading";

            for (int i = 0; i < 3; i++)
            {
                string prefix = (i == _selected ? "> " : "  ");
                _renderer.WriteLine(prefix + entries[i]);
            }

            _renderer.PassLine();
            _renderer.WriteLine("↑/↓ to navigate, ENTER to select.");
        }
        */
    }
}
