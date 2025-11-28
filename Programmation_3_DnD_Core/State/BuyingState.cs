using Programation_3_DnD.Composants;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Programation_3_DnD
{
    public class BuyingState : IState
    {
        private GameStateMachine _gameStateMachine;
        private IOutput _renderer;
        private GameObject _playerGameObject;
        private GameObject _merchantGameObject;

        private InventoryComposant _playerInventory;
        private InventoryComposant _merchantInventory;

        private List<ItemComposant> _items;
        private int _selectedIndex;
        private bool _shouldExit;

        //
        public BuyingState(GameStateMachine gameStateMachine, IOutput renderer, GameObject playerGameObject, GameObject merchantGameObject)
        {
            _gameStateMachine = gameStateMachine;
            _renderer = renderer;
            _playerGameObject = playerGameObject;
            _merchantGameObject = merchantGameObject;

            _playerInventory = _playerGameObject.GetComposant<InventoryComposant>();
            _merchantInventory = _merchantGameObject.GetComposant<InventoryComposant>();

            _items = new List<ItemComposant>();
            _selectedIndex = 0;
            _shouldExit = false;

            RefreshItems();
        }

        //
        private void RefreshItems()
        {
            _items.Clear();

            int count = _merchantInventory.GetItemCount();

            for (int i = 0; i < count; i++)
            {
                ItemComposant item = _merchantInventory.GetItemByIndex(i);
                if (item == null) continue;

                string name = item.GetName();

                if (name == "Gold") continue;

                int quantity = _merchantInventory.GetCountByIndex(i);
                if (quantity > 0)
                {
                    _items.Add(item);
                }
            }

            if (_selectedIndex >= _items.Count)
            {
                _selectedIndex = _items.Count - 1;
                if (_selectedIndex < 0)
                {
                    _selectedIndex = 0;
                }
            }
        }
        private void TryBuySelectedItem()
        {
            if (_items.Count == 0)
            {
                return;
            }

            if (_selectedIndex < 0 || _selectedIndex >= _items.Count)
            {
                return;
            }

            ItemComposant item = _items[_selectedIndex];
            if (item == null)
            {
                return;
            }

            string name = item.GetName();
            int price = item.GetPrice();

            int playerGold = _playerInventory.GetCount("Gold");
            if (playerGold < price)
            {
                return;
            }

            ItemComposant merchantItem = _merchantInventory.GetItemByName(name);
            if (merchantItem == null)
            {
                return;
            }

            int merchantCount = _merchantInventory.GetCount(name);
            if (merchantCount <= 0)
            {
                return;
            }

            _playerInventory.RemoveByName("Gold", price);

            ItemComposant goldPtr = _merchantInventory.GetItemByName("Gold");
            if (goldPtr != null)
            {
                _merchantInventory.Add(goldPtr, price);
            }

            _merchantInventory.RemoveByName(name, 1);

            _playerInventory.Add(item, 1);

            RefreshItems();
        }

        //
        public InventoryComposant GetPlayerInventory() { return _playerInventory; }
        public InventoryComposant GetMerchantInventory() { return _merchantInventory; }
        public int GetSelected() { return _selectedIndex; }

        //
        public void Enter() { }
        public void Exit() { }
        public void ProcessInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {
                _shouldExit = true;
            }
            else if (key == ConsoleKey.UpArrow)
            {
                if (_items.Count > 0)
                {
                    _selectedIndex--;
                    if (_selectedIndex < 0)
                    {
                        _selectedIndex = _items.Count - 1;
                    }
                }
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (_items.Count > 0)
                {
                    _selectedIndex++;
                    if (_selectedIndex >= _items.Count)
                    {
                        _selectedIndex = 0;
                    }
                }
            }
            else if (key == ConsoleKey.Enter)
            {
                TryBuySelectedItem();
            }
        }
        public void Update()
        {
            if (_shouldExit)
            {
                _gameStateMachine.SetState(
                    new TradingState(_gameStateMachine, _renderer, _playerGameObject, _merchantGameObject));
                return;
            }

            RefreshItems();
        }

        public void FixedUpdate(float delta)
        {
        }
        public void Render()
        {
           _renderer.RenderBuyingState(this);
        }


        /*
        public void Render()
        {
            _renderer.WriteLine("=== Acheter ===");

            int playerGold = _playerInventory.GetCount("Gold");
            int merchantGold = _merchantInventory.GetCount("Gold");

            _renderer.WriteLine("Votre or    : " + playerGold + " Gold");
            _renderer.WriteLine("Or marchand : " + merchantGold + " Gold");
            _renderer.PassLine();

            if (_items.Count == 0)
            {
                _renderer.WriteLine("Le marchand n'a rien à vendre.");
            }
            else
            {
                _renderer.WriteLine("Choisissez un objet (↑/↓), [ENTER] pour acheter :");
                _renderer.PassLine();

                for (int i = 0; i < _items.Count; i++)
                {
                    ItemComposant item = _items[i];
                    string prefix = (i == _selectedIndex) ? "> " : "  ";

                    int quantity = _merchantInventory.GetCount(item.GetName());

                    _renderer.WriteLine(prefix + item.GetName()
                        + " x" + quantity
                        + " (Prix : " + item.GetPrice() + " Gold)");
                }
            }

            _renderer.PassLine();
            _renderer.WriteLine("[ENTER] Acheter    [ESC] Retour");
        }
        */
    }
}
