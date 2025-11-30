using Programation_3_DnD_Core;

public class EventManagerTest
{
    private EventManager _eventManager;

    private TestEvent _simpleEvent;
    private TestTimedEvent _timedEvent;

    private class TestEvent : Event
    {
        public bool updated = false;

        public override void Update()
        {
            updated = true;
            SetIsCompleted();
        }
    }

    private class TestTimedEvent : TimedEvent
    {
        public int update_count = 0;

        public TestTimedEvent() : base(2) { }

        public override void Update()
        {
            update_count++;
            base.Update();
        }
        protected override void OnUpdate(float t)
        {

        }
    }

    [SetUp]
    public void Setup()
    {
        _eventManager = new EventManager();
        _simpleEvent = new TestEvent();
        _timedEvent = new TestTimedEvent();
    }

    [Test]
    public void RegisterEventDoesNotCrash()
    {
        _eventManager.RegisterEvent(_simpleEvent);
        Assert.Pass();
    }

    [Test]
    public void EventIsUpdatedWhenManagerUpdates()
    {
        _eventManager.RegisterEvent(_simpleEvent);
        _eventManager.Update();

        Assert.IsTrue(_simpleEvent.updated);
    }

    [Test]
    public void TimedEventIsRequeuedIfNotCompleted()
    {
        _eventManager.RegisterEvent(_timedEvent);

        _eventManager.Update();

        Assert.AreEqual(1, _timedEvent.update_count);
        Assert.IsFalse(_timedEvent.GetIsCompleted());
    }

    [Test]
    public void CompletedTimedEventIsNotRequeued()
    {
        _eventManager.RegisterEvent(_timedEvent);

        _eventManager.Update();
        _eventManager.Update();
        _eventManager.Update();

        Assert.AreEqual(3, _timedEvent.update_count);
    }
}
