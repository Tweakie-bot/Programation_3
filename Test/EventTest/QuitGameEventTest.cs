using NUnit.Framework;
using Programation_3_DnD.Engine;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;
using Programation_3_DnD.Output;

public class QuitGameEventTest
{
    private IOutput _renderer;
    private GameEngine _engine;
    private QuitGameEvent _quitEvent;

    [SetUp]
    public void Setup()
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "JsonTest");

        _renderer = new OutputManagerForTests();
        _engine = new GameEngine(_renderer, path);
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
