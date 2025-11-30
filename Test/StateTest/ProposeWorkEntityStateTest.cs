using Programation_3_DnD_Core;
using Programation_3_DnD_Console;
public class ProposeWorkEntityStateTest
{
    private IOutput _renderer;
    private InputProcessor _inputProcessor;

    private GameEngine _engine;
    private EventManager _eventManager;
    private GameManager _gameManager;
    private GameObject _player;
    private ProposeWorkEntityState _state;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _inputProcessor = new InputProcessor();

        _engine = new GameEngine(_renderer, _inputProcessor, path);
        _eventManager = _engine.GetEventManager();
        _gameManager = _engine.GetGameManager();
        _player = _gameManager.GetPlayer();

        _state = new ProposeWorkEntityState(_engine, _renderer, _eventManager, _player);
    }

    [Test]
    public void TryEnterDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Enter(); });
    }

    [Test]
    public void TryExitDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Exit(); });
    }

    [Test]
    public void TryUpdateDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Update(); });
    }

    [Test]
    public void TryRenderDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _state.Render(); });
    }

    [Test]
    public void TryProcessInputWithOtherKeyDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _inputProcessor.ChangeLastKeyForTests(ConsoleKey.A) ; _state.TreatInput(_inputProcessor) ; });
    }

    [Test]
    public void TryProcessInputWorkKeyDoesNotCrash()
    {
        Assert.DoesNotThrow(() => { _inputProcessor.ChangeLastKeyForTests(ConsoleKey.W) ; _state.TreatInput(_inputProcessor) ; });
    }
}
