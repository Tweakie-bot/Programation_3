using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Programation_3_DnD.Data
{
    public class ItemData
    {
        //
        [JsonInclude] private string _name;
        [JsonInclude] private int _valueInGold;
        [JsonInclude] private int _number;

        //
        public string GetName() { return _name; }
        public int GetValueInGold() { return _valueInGold; }
        public int GetNumber() { return _number; }
    }
}
