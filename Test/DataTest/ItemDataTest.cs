using NUnit.Framework;
using Programation_3_DnD.Data;
using System.Text.Json;

public class ItemDataTest
{
    private ItemData _data;

    [SetUp]
    public void Setup()
    {
        string json = @"
        {
          ""_name"": ""Potion de soin"",
          ""_valueInGold"": 25,
          ""_number"": 3
        }";

        _data = JsonSerializer.Deserialize<ItemData>(json);
    }

    [Test]
    public void NameIsCorrect()
    {
        Assert.AreEqual("Potion de soin", _data.GetName());
    }

    [Test]
    public void ValueInGoldIsCorrect()
    {
        Assert.AreEqual(25, _data.GetValueInGold());
    }

    [Test]
    public void NumberIsCorrect()
    {
        Assert.AreEqual(3, _data.GetNumber());
    }

    [Test]
    public void DataIsNotNullAfterDeserialization()
    {
        Assert.IsNotNull(_data);
    }

    [Test]
    public void GettersDoNotReturnDefaultValues()
    {
        Assert.AreNotEqual(null, _data.GetName());
        Assert.AreNotEqual(0, _data.GetValueInGold());
        Assert.AreNotEqual(0, _data.GetNumber());
    }
}
