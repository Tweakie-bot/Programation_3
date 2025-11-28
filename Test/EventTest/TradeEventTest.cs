using NUnit.Framework;
using Programation_3_DnD;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Objects;
using Programation_3_DnD.State;

public class TradeEventTest
{
    private IOutput _renderer;
    private GameEngine _engine;
    private EventManager _eventManager;
    private GameManager _gameManager;
    private GameStateMachine _gameStateMachine;

    private GameObject _player;
    private GameObject _merchant;

    private TradeEvent _tradeEvent;

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

        _tradeEvent = new TradeEvent(_gameStateMachine, _renderer, _player, _merchant);
    }

    [Test]
    public void TryNullStateMachineThrows()
    {
        TradeEvent trade_event = new TradeEvent(null, _renderer, _player, _merchant);

        Assert.Throws<System.Exception>(() => trade_event.Update());
    }

    [Test]
    public void TryNullRendererThrows()
    {
        TradeEvent trade_event = new TradeEvent(_gameStateMachine, null, _player, _merchant);

        Assert.Throws<System.Exception>(() => trade_event.Update());
    }

    [Test]
    public void TryNullPlayerThrows()
    {
        TradeEvent trade_event = new TradeEvent(_gameStateMachine, _renderer, null, _merchant);

        Assert.Throws<System.Exception>(() => trade_event.Update());
    }

    [Test]
    public void TryNullMerchantThrows()
    {
        TradeEvent trade_event = new TradeEvent(_gameStateMachine, _renderer, _player, null);

        Assert.Throws<System.Exception>(() => trade_event.Update());
    }
}
