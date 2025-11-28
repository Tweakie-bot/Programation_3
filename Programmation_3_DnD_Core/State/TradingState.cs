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
        public GameObject GetPlayer()
        {
            return _player;
        }
        public GameObject GetMerchant()
        {
            return _merchant;
        }
        public int GetSelected()
        {
            return _selected;
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
            _renderer.RenderTradingState(this);
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
