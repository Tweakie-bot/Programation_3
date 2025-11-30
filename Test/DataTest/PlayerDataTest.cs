using Newtonsoft.Json;
using Programation_3_DnD_Core;

public class PlayerDataTest
{
    private PlayerData _data;

    [SetUp]
    public void Setup()
    {
        string json = @"
        {
          ""_name"": ""Hero"",
          ""_inventory"": [
            {
              ""_itemName"": ""Gold"",
              ""_count"": 150
            },
            {
              ""_itemName"": ""Sword"",
              ""_count"": 1
            }
          ]
        }";

        _data = JsonConvert.DeserializeObject<PlayerData>(json);
    }

    [Test]
    public void NameIsCorrect()
    {
        Assert.AreEqual("Hero", _data.GetName());
    }

    [Test]
    public void InventoryCountIsCorrect()
    {
        Assert.AreEqual(2, _data.GetInventoryCount());
    }

    [Test]
    public void FirstItemNameIsCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(0);
        Assert.AreEqual("Gold", item.GetItemName());
    }

    [Test]
    public void FirstItemCountIsCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(0);
        Assert.AreEqual(150, item.GetCount());
    }

    [Test]
    public void SecondItemNameIsCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(1);
        Assert.AreEqual("Sword", item.GetItemName());
    }

    [Test]
    public void SecondItemCountIsCorrect()
    {
        PlayerItemEntry item = _data.GetItemAt(1);
        Assert.AreEqual(1, item.GetCount());
    }

    [Test]
    public void InventoryDoesNotCrashOnAccess()
    {
        for (int i = 0; i < _data.GetInventoryCount(); i++)
        {
            PlayerItemEntry entry = _data.GetItemAt(i);
            Assert.IsNotNull(entry);
        }
    }
}
