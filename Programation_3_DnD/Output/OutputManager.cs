using Programation_3_DnD_Core;
using Spectre.Console;
using Spectre.Console.Rendering;
using System.Text;

namespace Programation_3_DnD_Console
{
    public class OutputManager : IOutput
    {
        private StringBuilder _buffer = new StringBuilder();
        private bool _buffering = false;

        InventoryComposant _inventoryComposant = null;
        LocationComposant _locationComposant = null;

        public void WriteLine(string message)
        {
            if (_buffering)
            {
                _buffer.AppendLine(message);
            }
            else
            {
                AnsiConsole.WriteLine(message);
            }
        }

        public void PassLine()
        {
            WriteLine(string.Empty);
        }

        public void Clear()
        {
            AnsiConsole.Clear();
        }

        public void BeginFrame()
        {
            _buffer.Clear();
            _buffering = true;
        }

        public void EndFrame()
        {
            _buffering = false;

            AnsiConsole.Cursor.SetPosition(0, 0);

            AnsiConsole.Write(_buffer.ToString());
        }
        public void SetInventory(Composant composant)
        {
            if (composant as InventoryComposant != null)
            {
                _inventoryComposant = composant as InventoryComposant;
            }

            else
            {
                throw new Exception();
            }
        }
        public void SetLocation(LocationComposant location)
        {
            _locationComposant = location;
        }

        private IRenderable RenderInventoryPanel(bool exclude_gold = false)
        {
            if (_inventoryComposant != null)
            {
                Grid grid = new Grid();
                grid.AddColumn();

                if (_inventoryComposant.GetItemCount() == 0)
                {
                    grid.AddRow(new Markup("[grey]Inventaire vide[/]"));
                }
                else
                {
                    for (int i = 0; i < _inventoryComposant.GetItemCount(); i++)
                    {
                        ItemComposant item = _inventoryComposant.GetItemByIndex(i);
                        int count = _inventoryComposant.GetCountByIndex(i);

                        string line = $"[yellow]{item.GetName()}[/] x{count} - {item.GetPrice()}g";

                        if (item is WeaponComposant weapon)
                        {
                            line += $" ([red]{weapon.GetDamage()} dmg[/])";
                        }

                        grid.AddRow(new Markup(line));
                    }
                }

                return new Panel(grid).Header("[bold]bag of items[/]").Border(BoxBorder.Double);
            }

            else
            {
                return default;
            }
        }
        private IRenderable RenderLocationPanel()
        {
            if (_locationComposant != null)
            {
                Grid grid = new Grid();
                grid.AddColumn();
                grid.AddColumn();

                grid.AddRow(new Markup($"[bold yellow]{_locationComposant.GetName()}[/]"), new Markup($"[grey]{_locationComposant.GetDescription()}[/]"));

                if (_locationComposant.GetCount() > 0)
                {
                    for (int i = 0; i < _locationComposant.GetCount(); i++)
                    {
                        LocationComposant next_destination = _locationComposant.GetLocationAtIndex(i);
                        grid.AddRow(new Markup($"[green]{i + 1}[/]"), new Markup(next_destination.GetName()));
                    }
                }

                if (_locationComposant.GetPreviousLocation() != null)
                {
                    grid.AddRow(new Markup("[red]ESC[/]"), new Markup($"Go back to : {_locationComposant.GetPreviousLocation().GetName()}"));
                }

                if (_locationComposant.GetCharactersCount() > 0)
                {
                    foreach (GameObject character in _locationComposant.GetCopyOfCharacterTable())
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
            else
            {
                return default;
            }
        }
        public void RenderTradingState(TradingState state)
        {
            InventoryComposant player_inventory = state.GetPlayer().GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = state.GetMerchant().GetComposant<InventoryComposant>();

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
                if (i == state.GetSelected())
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

        public void RenderBuyingState(BuyingState state)
        {
            InventoryComposant player_inventory = state.GetPlayerInventory();
            InventoryComposant merchant_inventory = state.GetMerchantInventory();

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

            item_grid.AddRow(new Markup("[bold]Item[/]"), new Markup("[bold]Price[/]"), new Markup("[bold]Stock[/]"));

            int displayed_index = 0;

            for (int i = 0; i < merchant_inventory.GetItemCount(); i++)
            {
                ItemComposant item = merchant_inventory.GetItemByIndex(i);

                if (item.GetName() == "Gold")
                {
                    continue;
                }

                int count = merchant_inventory.GetCount(item.GetName());

                bool selected = displayed_index == state.GetSelected();

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

            Panel shop_panel = new Panel(item_grid).Header("[bold]Buy Items[/]").Border(BoxBorder.Double);

            main.AddRow(gold_panel, shop_panel);

            AnsiConsole.Write(main);
        }

        public void RenderSellingState(SellingState state)
        {
            InventoryComposant player_inventory = state.GetPlayer().GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = state.GetMerchant().GetComposant<InventoryComposant>();

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
                bool selected = displayed_index == state.GetSelected();

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

        public void RenderInGameState(InGameState state)
        {
            if (_locationComposant != null && _inventoryComposant != null)
            {
                PositionComposant position = state.GetPlayer().GetComposant<PositionComposant>();

                Panel header = new Panel(new Markup($"[bold]STATE :[/] [yellow]IN GAME[/]\n[grey]TIME[/] {state.GetTime():0.0}\n\n[[P]] Pause Menu")).Header("[bold]Status[/]").Border(BoxBorder.Rounded);

                IRenderable game_view = RenderLocationPanel();

                Layout layout = new Layout().SplitColumns(new Layout("left").Ratio(1), new Layout("right").Ratio(2));
                layout["left"].Update(header);

                IReadOnlyList<string> messages = state.GetGameEngine().GetUIMessages();

                if (messages.Count > 0)
                {
                    Grid message_grid = new Grid();
                    message_grid.AddColumn();

                    foreach (string message_grid_i in messages)
                    {
                        message_grid.AddRow(new Markup($"[green]{message_grid_i}[/]"));
                    }

                    Panel eventPanel = new Panel(message_grid).Header("[bold]Events[/]").Border(BoxBorder.Rounded);

                    layout["right"].Update(new Rows(game_view, eventPanel));
                }
                else
                {
                    layout["right"].Update(game_view);
                }

                AnsiConsole.Write(layout);
            }
        }

        public void RenderInventoryState(InventoryState state)
        {
            if (_inventoryComposant != null)
            {
                InventoryComposant inventory = state.GetPlayer().GetComposant<InventoryComposant>();

                Panel header = new Panel(new Markup("[bold yellow]INVENTORY[/] \n \n [grey][[Q]][/] Retour")).Header("[bold]Menu[/]").Border(BoxBorder.Rounded);

                int gold = inventory.GetCount("Gold");

                Panel gold_panel = new Panel(new Markup($"[yellow]Gold[/] \n [bold]{gold}[/]")).Header("[bold]$ [/]").Border(BoxBorder.Rounded);

                IRenderable inventory_panel = RenderInventoryPanel(exclude_gold: true);

                Layout right_layout = new Layout().SplitRows(new Layout("gold").Size(10), new Layout("items"));

                right_layout["gold"].Update(gold_panel);
                right_layout["items"].Update(inventory_panel);

                Layout layout = new Layout().SplitColumns(new Layout("left").Ratio(1), new Layout("right").Ratio(2));

                layout["left"].Update(header);
                layout["right"].Update(right_layout);

                AnsiConsole.Write(layout);
            }
        }

        public void RenderMainMenuState(MainMenuState state)
        {
            Panel menu = new Panel(new Markup("[bold yellow]THE DRAGONS OF STORMWRECK ISLAND[/]\n\n" + "[green][[ENTER]][/]\tPlay\n" + "[red][[ESC]][/]\tQuit"))
            .Header("[bold underline]MAIN MENU[/]")
            .BorderColor(Color.Grey)
            .Padding(2, 1);

            AnsiConsole.Write(Align.Center(menu));
        }

        public void RenderPauseMenuState(PauseMenuState state)
        {
            Panel menu = new Panel(new Markup("[bold yellow]PAUSE MENU[/]\n\n" + "[grey][[ESC]][/] Return to main menu\n" + "[grey][[P]][/] Resume game\n" + "[grey][[I]][/] Open inventory")).Header("[bold]Game Paused[/]").Border(BoxBorder.Double).Padding(2, 1);

            AnsiConsole.Write(Align.Center(menu, VerticalAlignment.Middle));
        }
    }
}
