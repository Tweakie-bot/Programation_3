using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Programation_3_DnD.Data
{
    public class LocationData
    {
        //
        [JsonInclude] private string _name { get; set; }
        [JsonInclude] private string _description { get; set; }
        [JsonInclude] private List<string> _nextLocations { get; set; }
        [JsonInclude] private List<string> _characters { get; set; }

        //
        public string GetName() { return _name; }
        public string GetDescription() { return _description; }
        public string GetLocationAt(int index) { return _nextLocations[index]; }
        public string GetCharacterAt(int index) { return _characters[index]; }
        public bool GetNextLocationNull()
        {
            if (_nextLocations.Count == 0)
            {
                return true;
            }
            return false;
        }
        public bool GetCharactersNull()
        {
            if (_characters.Count == 0)
            {
                return true;
            }
            return false;
        }
        public int GetNextLocationsCount() { return _nextLocations.Count; }
        public int GetCharactersCount() { return _characters.Count; }
    }
}
