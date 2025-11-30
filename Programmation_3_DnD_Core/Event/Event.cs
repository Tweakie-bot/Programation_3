
namespace Programation_3_DnD_Core
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
