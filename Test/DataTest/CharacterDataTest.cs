using Newtonsoft.Json;
using Programation_3_DnD_Core;

public class CharacterDataTest
{
    private CharacterData _data;

    [SetUp]
    public void Setup()
    {
        string json = @"
        {
          ""_name"": ""Mya"",
          ""_trade"": true,
          ""_work"": false,
          ""_inventory"": [
            {
              ""_itemName"": ""Gold"",
              ""_count"": 100
            },
            {
              ""_itemName"": ""Potion de soin"",
              ""_count"": 2
            }
          ]
        }";

        _data = JsonConvert.DeserializeObject<CharacterData>(json);
    }

    [Test]
    public void NameIsCorrect()
    {
        Assert.AreEqual("Mya", _data.GetName());
    }

    [Test]
    public void TradeFlagIsTrue()
    {
        Assert.IsTrue(_data.GetTrade());
    }

    [Test]
    public void WorkFlagIsFalse()
    {
        Assert.IsFalse(_data.GetWork());
    }

    [Test]
    public void InventoryCountCorrect()
    {
        Assert.AreEqual(2, _data.GetInventoryCount());
    }

    [Test]
    public void FirstItemNameCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(0);
        Assert.AreEqual("Gold", item.GetItemName());
    }

    [Test]
    public void FirstItemCountCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(0);
        Assert.AreEqual(100, item.GetCount());
    }

    [Test]
    public void SecondItemNameCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(1);
        Assert.AreEqual("Potion de soin", item.GetItemName());
    }

    [Test]
    public void SecondItemCountCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(1);
        Assert.AreEqual(2, item.GetCount());
    }
}
