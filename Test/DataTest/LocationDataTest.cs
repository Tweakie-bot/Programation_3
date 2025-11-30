using Newtonsoft.Json;
using Programation_3_DnD_Core;

public class LocationDataTest
{
    private LocationData _data;

    [SetUp]
    public void Setup()
    {
        string json = @"
        {
          ""_name"": ""Storm Island"",
          ""_description"": ""A dangerous island full of mysteries"",
          ""_nextLocations"": [""Dragon Rest"", ""Cloister""],
          ""_characters"": [""Mya"", ""Guardian""]
        }";

        _data = JsonConvert.DeserializeObject<LocationData>(json);
    }

    [Test]
    public void DataIsNotNull()
    {
        Assert.IsNotNull(_data);
    }

    [Test]
    public void NameIsCorrect()
    {
        Assert.AreEqual("Storm Island", _data.GetName());
    }

    [Test]
    public void DescriptionIsCorrect()
    {
        Assert.AreEqual("A dangerous island full of mysteries", _data.GetDescription());
    }

    [Test]
    public void NextLocationsCountIsCorrect()
    {
        Assert.AreEqual(2, _data.GetNextLocationsCount());
    }

    [Test]
    public void CharactersCountIsCorrect()
    {
        Assert.AreEqual(2, _data.GetCharactersCount());
    }

    [Test]
    public void GetLocationAtReturnsCorrectValue()
    {
        Assert.AreEqual("Dragon Rest", _data.GetLocationAt(0));
    }

    [Test]
    public void GetCharacterAtReturnsCorrectValue()
    {
        Assert.AreEqual("Mya", _data.GetCharacterAt(0));
    }

    [Test]
    public void NextLocationsIsNotNull()
    {
        Assert.IsFalse(_data.GetNextLocationNull());
    }

    [Test]
    public void CharactersIsNotNull()
    {
        Assert.IsFalse(_data.GetCharactersNull());
    }
}
