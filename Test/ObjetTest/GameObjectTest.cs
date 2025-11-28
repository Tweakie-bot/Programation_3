using NUnit.Framework;
using Programation_3_DnD.Objects;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Output;

public class GameObjectTest
{
    private GameObject _gameObject;
    private IOutput _renderer;

    [SetUp]
    public void Setup()
    {
        _renderer = new OutputManagerForTests();
        _gameObject = new GameObject();
    }

    [Test]
    public void AddComposantShouldAddComponent()
    {
        InventoryComposant inventory = new InventoryComposant(_renderer);

        _gameObject.AddComposant(inventory);

        Assert.IsTrue(_gameObject.IsContaining(inventory));
    }

    [Test]
    public void IsContainingReturnsFalse_WhenNotPresent()
    {
        InventoryComposant inventory = new InventoryComposant(_renderer);

        Assert.IsFalse(_gameObject.IsContaining(inventory));
    }

    [Test]
    public void GetComposantReturnsCorrectComponent()
    {
        InventoryComposant inventory = new InventoryComposant(_renderer);
        _gameObject.AddComposant(inventory);

        InventoryComposant result = _gameObject.GetComposant<InventoryComposant>();

        Assert.AreSame(inventory, result);
    }

    [Test]
    public void GetComposantThrowsWhenNotFound()
    {
        Assert.Throws<System.Exception>(() => { _gameObject.GetComposant<InventoryComposant>(); });
    }

    [Test]
    public void ProcessInputDoesNotCrash()
    {
        _gameObject.AddComposant(new InventoryComposant(_renderer));
        _gameObject.ProcessInput(System.ConsoleKey.A);

        Assert.Pass();
    }

    [Test]
    public void UpdateDoesNotCrash()
    {
        _gameObject.AddComposant(new InventoryComposant(_renderer));
        _gameObject.Update();

        Assert.Pass();
    }

    [Test]
    public void FixedUpdateDoesNotCrash()
    {
        _gameObject.AddComposant(new InventoryComposant(_renderer));
        _gameObject.FixedUpdate(1.5f);

        Assert.Pass();
    }

    [Test]
    public void RenderDoesNotCrash()
    {
        _gameObject.AddComposant(new InventoryComposant(_renderer));
        _gameObject.Render();

        Assert.Pass();
    }
}
