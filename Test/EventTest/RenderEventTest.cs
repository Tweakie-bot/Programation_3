using NUnit.Framework;
using Programation_3_DnD.Event;
using Programation_3_DnD.Interface;
using Programation_3_DnD.Manager;

public class RenderEventTest
{
    private IOutput _renderer;
    private RenderEvent _renderEvent;

    [SetUp]
    public void Setup()
    {
        _renderer = new OutputManagerForTests();
        _renderEvent = new RenderEvent(_renderer, "Test message", 0.1f);
    }

    [Test]
    public void TryCallingUpdateDoesNotCrash()
    {
        _renderEvent.Update();
        Assert.Pass();
    }

    [Test]
    public void RenderEventIsNotCompletedByDefault()
    {
        Assert.IsFalse(_renderEvent.GetIsCompleted());
    }

    [Test]
    public void RenderEventCanBeManuallyCompleted()
    {
        _renderEvent.SetIsCompleted();
        Assert.IsTrue(_renderEvent.GetIsCompleted());
    }
}
