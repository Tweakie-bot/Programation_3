using Programation_3_DnD.Data;

public class WeaponComposant : ItemComposant
{
    //
    private int _damage;

    //
    public WeaponComposant(string name, int value, int damage) : base(name, value)
    {
        _damage = damage;
    }
    public WeaponComposant(WeaponData data) : base(data.GetName(), data.GetValueInGold())
    {
        data.GetDamage();
    }

    //
    public int GetDamage()
    {
        return _damage;
    }
}
