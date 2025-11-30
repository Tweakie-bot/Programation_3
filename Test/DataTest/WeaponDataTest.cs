using Newtonsoft.Json;
using Programation_3_DnD_Core;

public class WeaponDataTest
{
    private WeaponData _data;

    [SetUp]
    public void Setup()
    {
        string json = @"
        {
          ""_name"": ""Sword"",
          ""_valueInGold"": 150,
          ""_number"": 1,
          ""_damage"": 25
        }";

        _data = JsonConvert.DeserializeObject<WeaponData>(json);
    }

    [Test]
    public void DataIsNotNull()
    {
        Assert.IsNotNull(_data);
    }

    [Test]
    public void WeaponNameIsCorrect()
    {
        Assert.AreEqual("Sword", _data.GetName());
    }

    [Test]
    public void WeaponValueIsCorrect()
    {
        Assert.AreEqual(150, _data.GetValueInGold());
    }

    [Test]
    public void WeaponNumberIsCorrect()
    {
        Assert.AreEqual(1, _data.GetNumber());
    }

    [Test]
    public void WeaponDamageIsCorrect()
    {
        Assert.AreEqual(25, _data.GetDamage());
    }
}
