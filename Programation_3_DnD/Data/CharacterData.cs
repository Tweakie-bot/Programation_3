using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Programation_3_DnD.Data
{
    public class CharacterData
    {
        //
        [JsonInclude] private string _name;
        [JsonInclude] private bool _trade;
        [JsonInclude] private bool _work;
        [JsonInclude] private List<PlayerItemEntry> _inventory;

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
