using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.StateTest
{
    public class SellingStateTest
    {
        public IOutput _renderer;
        public InputProcessor _inputProcessor;

        public GameEngine _engine;
        public EventManager _eventManager;
        public GameManager _gameManager;
        public GameStateMachine _gameStateMachine;
        public GameObject _player;
        public GameObject _merchant;
        public SellingState _sellingState;

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

            merchant_inventory.Add(new ItemComposant("Potion de soin", 10), 2);
            merchant_inventory.Add(new ItemComposant("Gold", 1), 200);

            _merchant.AddComposant(merchant_inventory);

            _sellingState = new SellingState(_gameStateMachine, _renderer, _player, _merchant);
            _gameStateMachine.SetState(_sellingState);
        }

        [Test]
        public void TrySelectionAndSell()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.DownArrow);
            _sellingState.TreatInput(_inputProcessor);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _sellingState.TreatInput(_inputProcessor);

            InventoryComposant inventory = _player.GetComposant<InventoryComposant>();
            Assert.AreEqual(2, inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TrySellNoGold()
        {
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();
            merchant_inventory.RemoveByName("Gold", 50);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _sellingState.TreatInput(_inputProcessor);

            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            Assert.AreEqual(3, player_inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TryItemExchange()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _sellingState.TreatInput(_inputProcessor);

            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();

            Assert.AreEqual(3, player_inventory.GetCount("Potion de soin"));
            Assert.AreEqual(2, merchant_inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TryGoldExchange()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _sellingState.TreatInput(_inputProcessor);

            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();

            Assert.AreEqual(35, player_inventory.GetCount("Gold"));
            Assert.AreEqual(190, merchant_inventory.GetCount("Gold"));
        }

        [Test]
        public void TryGoingTradingState()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Escape);
            _sellingState.TreatInput(_inputProcessor);

            _sellingState.Update();

            Assert.IsInstanceOf<TradingState>(_gameStateMachine.GetCurrentState());
        }

    }
}