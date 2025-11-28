using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Programation_3_DnD.Data
{
    public class PlayerData
    {
        //
        [JsonInclude] private string _name;
        [JsonInclude] private List<PlayerItemEntry> _inventory;

        //
        public PlayerData() { }

        //
        public string GetName() { return _name; }
        public PlayerItemEntry GetItemAt(int index) { return _inventory[index]; }
        public int GetInventoryCount() { return _inventory.Count; }
    }

    public class PlayerItemEntry
    {
        [JsonInclude] private string _itemName;
        [JsonInclude] private int _count;

        //
        public PlayerItemEntry() { }

        //
        public string GetItemName() { return _itemName; }
        public int GetCount() { return _count; }
    }
}

