using NUnit;
using Programation_3_DnD;
using Programation_3_DnD.Composants;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace Test.StateTest
{
    public class TradingStateTest
    {
        public IOutput _renderer;
        public GameEngine _engine;
        public EventManager _eventManager;
        public GameManager _gameManager;
        public GameStateMachine _gameStateMachine;
        public GameObject _player;
        public GameObject _merchant;
        public TradingState _tradingState;

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
            merchant_inventory.Add(new ItemComposant("Gold", 1), 200);
            merchant_inventory.Add(new ItemComposant("Potion de soin", 10), 3);

            _merchant.AddComposant(merchant_inventory);

            _tradingState = new TradingState(_gameStateMachine, _renderer, _player, _merchant);
        }

        [Test]
        public void TryEnteringBuyingState()
        {
            _tradingState.ProcessInput(ConsoleKey.Enter);
            Assert.IsInstanceOf<BuyingState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryEnteringSellingState()
        {
            _tradingState.ProcessInput(ConsoleKey.DownArrow);
            _tradingState.ProcessInput(ConsoleKey.Enter);

            Assert.IsInstanceOf<SellingState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryQuittingState()
        {
            _tradingState.ProcessInput(ConsoleKey.UpArrow);
            _tradingState.ProcessInput(ConsoleKey.Enter);

            Assert.IsInstanceOf<InGameState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void Navigation()
        {
            _tradingState.ProcessInput(ConsoleKey.DownArrow);
            _tradingState.ProcessInput(ConsoleKey.DownArrow);
            _tradingState.ProcessInput(ConsoleKey.DownArrow);
            _tradingState.ProcessInput(ConsoleKey.Enter);

            Assert.IsInstanceOf<BuyingState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryDisplayGold()
        {
            int player_gold = _player.GetComposant<InventoryComposant>().GetCount("Gold");
            int merchant_gold = _merchant.GetComposant<InventoryComposant>().GetCount("Gold");

            Assert.AreEqual(25, player_gold);
            Assert.AreEqual(200, merchant_gold);
        }

    }
}