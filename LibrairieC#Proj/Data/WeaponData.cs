using Newtonsoft.Json;

namespace Programation_3_DnD_Core
{
    public class WeaponData : ItemData
    {
        //
        [JsonProperty ("_damage")] private int _damage;

        //
        public WeaponData() { }

        //
        public int GetDamage() { return _damage; }
    }
}
