using Newtonsoft.Json;
using System.Collections.Generic;

namespace Programation_3_DnD_Core
{
    public class LocationData
    {
        //
        [JsonProperty ("_name")] private string _name { get; set; }
        [JsonProperty ("_description")] private string _description { get; set; }
        [JsonProperty ("_nextLocations")] private List<string> _nextLocations { get; set; }
        [JsonProperty ("_characters")] private List<string> _characters { get; set; }

        //
        public LocationData() { }

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
