using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

public class GameObjectTest
{
    private IOutput _renderer;
    private InputProcessor _inputProcessor;

    private GameObject _gameObject;

    [SetUp]
    public void Setup()
    {
        _inputProcessor = new InputProcessor();
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
        _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A);
        _gameObject.TreatInput(_inputProcessor);

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
