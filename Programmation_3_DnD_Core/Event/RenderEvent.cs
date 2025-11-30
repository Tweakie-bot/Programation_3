
namespace Programation_3_DnD_Core
{
    public class RenderEvent : TimedEvent
    {
        //
        private readonly IOutput _renderer;

        private readonly string _message;

        //
        public RenderEvent(IOutput renderer, string message, float duration = 2f) : base(duration)
        {
            _renderer = renderer;
            _message = message;
        }

        //
        protected override void OnUpdate(float elapsed_seconds)
        {
            _renderer.WriteLine(_message);
        }
        protected override void OnFinish()
        {
            _renderer.WriteLine("(message faded)\n");
        }
    }

}
