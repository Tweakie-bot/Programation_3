using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Programation_3_DnD.Data
{
    public class WeaponData : ItemData
    {
        //
        [JsonInclude] private int _damage;

        //
        public int GetDamage() { return _damage; }
    }
}
