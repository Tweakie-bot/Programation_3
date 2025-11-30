using System.Diagnostics;

namespace Programation_3_DnD_Core
{
    public abstract class TimedEvent : Event
    {
        //
        private readonly Stopwatch _stopWatch = new Stopwatch();

        private readonly float _duration;

        //
        protected TimedEvent(float duration)
        {
            _duration = duration;
        }

        //
        protected abstract void OnUpdate(float elapsed_seconds);
        protected virtual void OnFinish() { }

        //
        public override bool GetIsCompleted()
        {
            return _isCompleted;
        }

        //
        public override void Update()
        {
            if (!_stopWatch.IsRunning)
                _stopWatch.Start();

            OnUpdate(_stopWatch.ElapsedMilliseconds / 1000f);

            if (_stopWatch.ElapsedMilliseconds >= _duration * 1000)
            {
                OnFinish();
                _isCompleted = true;
            }
        }
    }
}
