using Programation_3_DnD.Composants;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Programation_3_DnD.State
{
    public class SellingState : IState
    {
        private GameStateMachine _machine;
        private IOutput _renderer;
        private GameObject _player;
        private GameObject _merchant;
        private InventoryComposant _playerInv;
        private InventoryComposant _merchantInv;

        private List<ItemComposant> _items;

        private int _selected;

        private bool _exit;

        //
        public SellingState(GameStateMachine machine, IOutput renderer, GameObject player, GameObject merchant)
        {
            _machine = machine;
            _renderer = renderer;
            _player = player;
            _merchant = merchant;

            _playerInv = _player.GetComposant<InventoryComposant>();
            _merchantInv = _merchant.GetComposant<InventoryComposant>();

            _items = new List<ItemComposant>();
            _selected = 0;
            _exit = false;

            RefreshItems();
        }

        //
        private void RefreshItems()
        {
            _items.Clear();

            int count = _playerInv.GetItemCount();
            for (int i = 0; i < count; i++)
            {
                ItemComposant item = _playerInv.GetItemByIndex(i);

                if (item.GetName() == "Gold")
                {
                    continue;
                }

                int qty = _playerInv.GetCount(item.GetName());
                if (qty > 0)
                {
                    _items.Add(item);
                }
            }

            if (_selected >= _items.Count)
            {
                _selected = _items.Count - 1;
                if (_selected < 0)
                {
                    _selected = 0;
                }
            }
        }
        private void TrySell()
        {
            if (_items.Count == 0) return;
            if (_selected < 0 || _selected >= _items.Count) return;

            ItemComposant item = _items[_selected];
            int qty = _playerInv.GetCount(item.GetName());
            if (qty <= 0) return;

            int price = item.GetPrice();
            int merchantGold = _merchantInv.GetCount("Gold");

            if (merchantGold < price)
            {
                return;
            }

            _playerInv.RemoveByName(item.GetName(), 1);
            _merchantInv.Add(item, 1);

            _merchantInv.RemoveByName("Gold", price);
            _playerInv.AddByName("Gold", price);

            RefreshItems();
        }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {
                _exit = true;
                return;
            }

            if (key == ConsoleKey.UpArrow)
            {
                _selected--;
                if (_selected < 0) _selected = _items.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                _selected++;
                if (_selected >= _items.Count) _selected = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                TrySell();
            }
        }
        public void Update()
        {
            if (_exit)
            {
                _machine.SetState(new TradingState(_machine, _renderer, _player, _merchant));
                return;
            }

            RefreshItems();
        }
        public void FixedUpdate(float delta) { }
        public void Render()
        {
            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();

            int player_gold = player_inventory.GetCount("Gold");
            int merchant_gold = merchant_inventory.GetCount("Gold");

            Grid main = new Grid();
            main.AddColumn();
            main.AddColumn();

            Grid gold_grid = new Grid();
            gold_grid.AddColumn();
            gold_grid.AddColumn();

            gold_grid.AddRow(new Markup("[yellow]Player Gold[/]"), new Markup("[yellow]Merchant Gold[/]"));

            gold_grid.AddRow(new Markup($"[bold]{player_gold}[/]"), new Markup($"[bold]{merchant_gold}[/]"));

            Panel gold_panel = new Panel(gold_grid).Header("[bold]Gold[/]").Border(BoxBorder.Rounded);

            Grid item_grid = new Grid();
            item_grid.AddColumn();
            item_grid.AddColumn();
            item_grid.AddColumn();

            item_grid.AddRow(new Markup("[bold]Item[/]"), new Markup("[bold]Price[/]"), new Markup("[bold]Quantity[/]"));

            int displayed_index = 0;

            for (int i = 0; i < player_inventory.GetItemCount(); i++)
            {
                ItemComposant item = player_inventory.GetItemByIndex(i);

                if (item.GetName() == "Gold")
                {
                    continue;
                }

                int count = player_inventory.GetCount(item.GetName());
                bool selected = displayed_index == _selected;

                if (selected)
                {
                    item_grid.AddRow(new Markup($"[black on yellow]> {item.GetName()} <[/]"), new Markup($"[black on yellow]{item.GetPrice()}[/]"), new Markup($"[black on yellow]{count}[/]"));
                }

                else
                {
                    item_grid.AddRow(new Markup(item.GetName()), new Markup(item.GetPrice().ToString()), new Markup(count.ToString()));
                }

                displayed_index++;
            }

            Panel sell_panel = new Panel(item_grid).Header("[bold]Sell Items[/]").Border(BoxBorder.Double);

            main.AddRow(gold_panel, sell_panel);

            AnsiConsole.Write(main);
        }

        /*
        public void Render()
        {
            _renderer.WriteLine("=== SELL ===");
            _renderer.WriteLine("Your gold    : " + _playerInv.GetCount("Gold"));
            _renderer.WriteLine("Merchant's gold : " + _merchantInv.GetCount("Gold"));
            _renderer.PassLine();

            if (_items.Count == 0)
            {
                _renderer.WriteLine("You have nothing to sell.");
            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    ItemComposant item = _items[i];
                    string prefix = (i == _selected ? "> " : "  ");
                    _renderer.WriteLine(prefix + item.GetName() + " x" + _playerInv.GetCount(item.GetName()) + " (Value : " + item.GetPrice() + ")");
                }
            }
            _renderer.PassLine();
            _renderer.WriteLine("[ENTER] Sell   [ESC] Back");
        }
        */
    }
}
