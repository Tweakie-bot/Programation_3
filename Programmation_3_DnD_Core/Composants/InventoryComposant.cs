using System;
using System.Collections.Generic;
using Programation_3_DnD.Interface;
using Spectre.Console.Rendering;
using Spectre.Console;

namespace Programation_3_DnD.Composants
{
    public class InventoryComposant : Composant
    {
        // Variables
        private List<ItemComposant> _items;
        private List<int> _counts;
        private IOutput _renderer;

        // Constructeur
        public InventoryComposant(IOutput renderer)
        {
            _renderer = renderer;
            _items = new List<ItemComposant>();
            _counts = new List<int>();
        }

        // Méthodes
        public void Add(ItemComposant item, int count)
        {
            if (item == null) return;
            if (count <= 0) return;

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == item)
                {
                    _counts[i] += count;
                    return;
                }
            }
            _items.Add(item);
            _counts.Add(count);
        }
        public bool AddByName(string name, int count)
        {
            if (count < 1)
            {
                return false;
            }

            for (int i = 0 ; i < _counts.Count; i++)
            {
                if (_items[i].GetName() == name)
                {
                    _counts[i] += count;
                    return true;
                }
            }

            return false;
        }
        public bool RemoveByName(string item_name, int count)
        {
            if (count < 1) return false;

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].GetName() == item_name)
                {
                    if (_counts[i] < count) return false;

                    _counts[i] -= count;
                    if (_counts[i] == 0)
                    {
                        _items.RemoveAt(i);
                        _counts.RemoveAt(i);
                    }
                    return true;
                }
            }

            return false;
        }
        public bool RemoveAtIndex(int index, int count)
        {
            if (index < 0 || index >= _items.Count) return false;
            if (count <= 0) return false;

            if (_counts[index] < count) return false;

            _counts[index] -= count;
            if (_counts[index] == 0)
            {
                _items.RemoveAt(index);
                _counts.RemoveAt(index);
            }

            return true;
        }

        // Getters
        public int GetCount(string item_name)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].GetName() == item_name)
                {
                    return _counts[i];
                }
            }

            return 0;
        }
        public int GetCountByIndex(int index)
        {
            if (index < 0 || index >= _counts.Count)
            {
                return 0;
            }

            return _counts[index];
        }
        public ItemComposant GetItemByName(string item_name)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].GetName() == item_name)
                {
                    return _items[i];
                }
            }
            return null;
        }
        public ItemComposant GetItemByIndex(int index)
        {
            if (index < 0 || index >= _items.Count)
            {
                return null;
            }

            return _items[index];
        }
        public int GetItemCount()
        {
            return _items.Count;
        }

        // Logique
        public override void ProcessInput(ConsoleKey key) { }
        public override void Update() { }
        public override void FixedUpdate(float time) { }
        public IRenderable RenderInventoryPanel(bool exclude_gold = false)
    {
            Grid grid = new Grid();
            grid.AddColumn();

            if (_items.Count == 0)
            {
                grid.AddRow(new Markup("[grey]Inventaire vide[/]"));
            }
            else
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    ItemComposant item = _items[i];
                    int count = _counts[i];

                    string line =$"[yellow]{item.GetName()}[/] x{count} - {item.GetPrice()}g";

                    if (item is WeaponComposant weapon)
                    {
                        line += $" ([red]{weapon.GetDamage()} dmg[/])";
                    }

                    grid.AddRow(new Markup(line));
                }
            }

            return new Panel(grid).Header("[bold]bag of items[/]").Border(BoxBorder.Double);
        }

    public override void Render()
        {
            /*
            if (_items.Count == 0)
            {
                _renderer.WriteLine("Inventaire vide.");
                return;
            }

            _renderer.WriteLine("=== Inventaire ===");

            for (int i = 0; i < _items.Count; i++)
            {
                ItemComposant item = _items[i];
                int count = _counts[i];

                _renderer.WriteLine(item.GetName());
                _renderer.WriteLine("Prix : " + item.GetPrice());
                _renderer.WriteLine("Quantité : " + count);

                WeaponComposant weapon = item as WeaponComposant;
                if (weapon != null)
                {
                    _renderer.WriteLine("Dégâts : " + weapon.GetDamage());
                }

                _renderer.PassLine();
            } */
        }
    }
}
