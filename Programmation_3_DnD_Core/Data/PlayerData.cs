using Newtonsoft.Json;
using System.Collections.Generic;

namespace Programation_3_DnD_Core
{
    public class PlayerData
    {
        //
        [JsonProperty ("_name")] private string _name;
        [JsonProperty ("_inventory")] private List<PlayerItemEntry> _inventory;

        //
        public PlayerData() { }

        //
        public string GetName() { return _name; }
        public PlayerItemEntry GetItemAt(int index) { return _inventory[index]; }
        public int GetInventoryCount() { return _inventory.Count; }
    }

    public class PlayerItemEntry
    {
        [JsonProperty ("_itemName")] private string _itemName;
        [JsonProperty ("_count")] private int _count;

        //
        public PlayerItemEntry() { }

        //
        public string GetItemName() { return _itemName; }
        public int GetCount() { return _count; }
    }
}

