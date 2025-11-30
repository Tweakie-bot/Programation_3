using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

public class BuyingStateTest
{
    private IOutput _renderer;
    private InputProcessor _inputProcessor;

    private GameEngine _engine;
    private EventManager _eventManager;
    private GameManager _gameManager;
    private GameStateMachine _gameStateMachine;

    private GameObject _player;
    private GameObject _merchant;
    IState _state;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _inputProcessor = new InputProcessor();

        _engine = new GameEngine(_renderer, _inputProcessor, path);
        _eventManager = _engine.GetEventManager();
        _gameManager = _engine.GetGameManager();
        _gameStateMachine = _engine.GetGameStateMachine();

        _player = _gameManager.GetPlayer();
        _merchant = new GameObject();

        InventoryComposant merchant_inventory = new InventoryComposant(_renderer);

        ItemComposant gold = new ItemComposant("Gold", 1);
        ItemComposant potion = new ItemComposant("Potion de soin", 10);

        merchant_inventory.Add(gold, 50);
        merchant_inventory.Add(potion, 3);

        _merchant.AddComposant(merchant_inventory);

        _state = new BuyingState(_gameStateMachine, _renderer, _player, _merchant);
        _gameStateMachine.SetState(_state);
    }

    [Test]
    public void TryNoCrashGoingDown()
    {
        _inputProcessor.ChangeLastKeyForTests(ConsoleKey.DownArrow);
        _state.TreatInput(_inputProcessor);
        Assert.Pass();
    }

    [Test]
    public void TryNoCrashGoingUp()
    {
        _inputProcessor.ChangeLastKeyForTests(ConsoleKey.UpArrow);
        _state.TreatInput(_inputProcessor);
        Assert.Pass();
    }

    [Test]
    public void TryMerchantStockChanged()
    {
        _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
        _state.TreatInput(_inputProcessor);

        InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();
        Assert.AreEqual(2, merchant_inventory.GetCount("Potion de soin"));
    }

    [Test]
    public void TryGoldTransfer()
    {
        _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
        _state.TreatInput(_inputProcessor);

        InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
        InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();

        Assert.AreEqual(15, player_inventory.GetCount("Gold"));
        Assert.AreEqual(60, merchant_inventory.GetCount("Gold"));
    }

    [Test]
    public void TryNoGold()
    {
        InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
        player_inventory.RemoveByName("Gold", 100);

        _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
        _state.TreatInput(_inputProcessor);

        Assert.AreEqual(2, _merchant.GetComposant<InventoryComposant>().GetCount("Potion de soin"));
    }

    [Test]
    public void TryEscape()
    {
        _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Escape);
        _state.TreatInput(_inputProcessor);
        _state.Update();

        Assert.IsInstanceOf<TradingState>(_gameStateMachine.GetCurrentState());
    }
}

