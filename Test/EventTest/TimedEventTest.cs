using Programation_3_DnD_Core;

public class TimedEventTest
{
    private TestTimedEvent _timedEvent;

    private class TestTimedEvent : TimedEvent
    {
        public float LastElapsed;

        public TestTimedEvent(float duration) : base(duration) { }

        protected override void OnUpdate(float elapsed_seconds)
        {
            LastElapsed = elapsed_seconds;
        }

        protected override void OnFinish()
        {

        }
    }

    [SetUp]
    public void Setup()
    {
        _timedEvent = new TestTimedEvent(0.1f);
    }

    [Test]
    public void TryCallingUpdateDoesNotCrash()
    {
        _timedEvent.Update();
        Assert.Pass();
    }

    [Test]
    public void IsNotCompletedAtStart()
    {
        Assert.IsFalse(_timedEvent.GetIsCompleted());
    }

    [Test]
    public void OnUpdateReceivesElapsedTime()
    {
        _timedEvent.Update();
        Assert.GreaterOrEqual(_timedEvent.LastElapsed, 0f);
    }

    [Test]
    public void MultipleUpdatesIncreaseElapsed()
    {
        _timedEvent.Update();
        float first = _timedEvent.LastElapsed;

        _timedEvent.Update();
        float second = _timedEvent.LastElapsed;

        Assert.GreaterOrEqual(second, first);
    }
}
