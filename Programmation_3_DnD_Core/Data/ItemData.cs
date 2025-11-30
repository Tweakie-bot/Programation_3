using Newtonsoft.Json;

namespace Programation_3_DnD_Core
{
    public class ItemData
    {
        //
        [JsonProperty ("_name")] private string _name;
        [JsonProperty ("_valueInGold")] private int _valueInGold;
        [JsonProperty ("_number")] private int _number;

        public ItemData() { }

        //
        public string GetName() { return _name; }
        public int GetValueInGold() { return _valueInGold; }
        public int GetNumber() { return _number; }
    }
}
