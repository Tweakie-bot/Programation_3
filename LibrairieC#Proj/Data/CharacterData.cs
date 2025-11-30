using Newtonsoft.Json;
using System.Collections.Generic;

namespace Programation_3_DnD_Core
{
    public class CharacterData
    {
        //
        [JsonProperty ("_name")] private string _name;
        [JsonProperty ("_trade")] private bool _trade;
        [JsonProperty ("_work")] private bool _work;
        [JsonProperty ("_inventory")] private List<PlayerItemEntry> _inventory;

        //
        public CharacterData() { }

        //
        public string GetName() { return _name; }
        public bool GetTrade() { return _trade; }
        public bool GetWork() { return _work; }
        public PlayerItemEntry GetItemAt(int index) { return _inventory[index]; }
        public int GetInventoryCount() { return _inventory.Count; }
    }
}
