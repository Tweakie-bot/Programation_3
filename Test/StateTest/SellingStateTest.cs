using NUnit;
using Programation_3_DnD;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;

namespace Test.StateTest
{
    public class SellingStateTest
    {
        public IOutput _renderer;
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

            _engine = new GameEngine(_renderer, path);
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
            _sellingState.ProcessInput(ConsoleKey.DownArrow);
            _sellingState.ProcessInput(ConsoleKey.Enter);

            InventoryComposant inventory = _player.GetComposant<InventoryComposant>();
            Assert.AreEqual(2, inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TrySellNoGold()
        {
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();
            merchant_inventory.RemoveByName("Gold", 50);

            _sellingState.ProcessInput(ConsoleKey.Enter);

            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            Assert.AreEqual(3, player_inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TryItemExchange()
        {
            _sellingState.ProcessInput(ConsoleKey.Enter);

            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();

            Assert.AreEqual(3, player_inventory.GetCount("Potion de soin"));
            Assert.AreEqual(2, merchant_inventory.GetCount("Potion de soin"));
        }

        [Test]
        public void TryGoldExchange()
        {
            _sellingState.ProcessInput(ConsoleKey.Enter);

            InventoryComposant player_inventory = _player.GetComposant<InventoryComposant>();
            InventoryComposant merchant_inventory = _merchant.GetComposant<InventoryComposant>();

            Assert.AreEqual(35, player_inventory.GetCount("Gold"));
            Assert.AreEqual(190, merchant_inventory.GetCount("Gold"));
        }

        [Test]
        public void TryGoingTradingState()
        {
            _sellingState.ProcessInput(ConsoleKey.Escape);
            _sellingState.Update();

            Assert.IsInstanceOf<TradingState>(_gameStateMachine.GetCurrentState());
        }

    }
}