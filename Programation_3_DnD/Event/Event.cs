using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Event
{
    public abstract class Event
    {
        //
        protected bool _isCompleted = false;

        //
        public void SetIsCompleted()
        {
            _isCompleted = true;
        }

        //
        public virtual bool GetIsCompleted()
        {
            return _isCompleted;
        }

        //
        public abstract void Update();
    }
}
