using NUnit.Framework;
using Programation_3_DnD.Event;

public class EventTest
{
    private TestEvent _event;

    private class TestEvent : Event
    {
        public override void Update()
        {
            SetIsCompleted();
        }
    }

    [SetUp]
    public void Setup()
    {
        _event = new TestEvent();
    }

    [Test]
    public void InitialStateIsNotCompleted()
    {
        Assert.IsFalse(_event.GetIsCompleted());
    }

    [Test]
    public void SetIsCompletedSetsToTrue()
    {
        _event.SetIsCompleted();
        Assert.IsTrue(_event.GetIsCompleted());
    }

    [Test]
    public void UpdateMarksEventAsCompleted()
    {
        _event.Update();
        Assert.IsTrue(_event.GetIsCompleted());
    }

    [Test]
    public void EventInstanceIsNotNull()
    {
        Assert.IsNotNull(_event);
    }
}
