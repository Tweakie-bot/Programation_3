using Programation_3_DnD_Console;
using Programation_3_DnD_Core;

public class QuitGameEventTest
{
    private IOutput _renderer;
    private InputProcessor _inputProcessor;
    private GameEngine _engine;
    private QuitGameEvent _quitEvent;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _inputProcessor = new InputProcessor();

        _engine = new GameEngine(_renderer, _inputProcessor, path);
        _quitEvent = new QuitGameEvent(_engine);
    }

    [Test]
    public void TryCallingQuitEventDoesNotCrash()
    {
        _quitEvent.Update();
        Assert.Pass();
    }

    [Test]
    public void QuitEventCanBeMarkedCompleted()
    {
        _quitEvent.Update();
        _quitEvent.SetIsCompleted();

        Assert.IsTrue(_quitEvent.GetIsCompleted());
    }

    [Test]
    public void QuitEventIsNotCompletedByDefault()
    {
        Assert.IsFalse(_quitEvent.GetIsCompleted());
    }
}
