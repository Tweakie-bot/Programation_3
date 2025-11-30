
using System;
using System.Collections.Generic;

namespace Programation_3_DnD_Core
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
        public GameObject GetPlayer() { return _player; }
        public GameObject GetMerchant() { return _merchant; }
        public int GetSelected() { return _selected; }

        //
        public void Enter() { }
        public void Exit() { }
        public void TreatInput(IInput input_manager)
        {
            if (input_manager.IsKeyCancel())
            {
                _exit = true;
                return;
            }

            if  (input_manager.IsKeyUp())
            {
                _selected--;
                if (_selected < 0) _selected = _items.Count - 1;
            }
            else if (input_manager.IsKeyDown())
            {
                _selected++;
                if (_selected >= _items.Count) _selected = 0;
            }
            else if (input_manager.IsKeyValidate())
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
           _renderer.RenderSellingState(this);
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
