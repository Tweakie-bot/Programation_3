using Programation_3_DnD.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Event
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
