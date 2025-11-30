using Programation_3_DnD_Core;
using Programation_3_DnD_Console;

namespace Test.StateTest
{
    public class TradingStateTest
    {
        public IOutput _renderer;
        public InputProcessor _inputProcessor;

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
            _inputProcessor = new InputProcessor();

            _engine = new GameEngine(_renderer, _inputProcessor, path);
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
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _tradingState.TreatInput(_inputProcessor);

            Assert.IsInstanceOf<BuyingState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryEnteringSellingState()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.DownArrow);
            _tradingState.TreatInput(_inputProcessor);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _tradingState.TreatInput(_inputProcessor);

            Assert.IsInstanceOf<SellingState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void TryQuittingState()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.UpArrow);
            _tradingState.TreatInput(_inputProcessor);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _tradingState.TreatInput(_inputProcessor);

            Assert.IsInstanceOf<InGameState>(_gameStateMachine.GetCurrentState());
        }

        [Test]
        public void Navigation()
        {
            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.DownArrow);
            _tradingState.TreatInput(_inputProcessor);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.DownArrow);
            _tradingState.TreatInput(_inputProcessor);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.DownArrow);
            _tradingState.TreatInput(_inputProcessor);

            _inputProcessor.ChangeLastKeyForTests(ConsoleKey.Enter);
            _tradingState.TreatInput(_inputProcessor);

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