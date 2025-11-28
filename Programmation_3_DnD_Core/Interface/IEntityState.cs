using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Interface
{
    public interface IEntityState
    {
        //
        public void Enter();
        public void Exit();

        //
        public void ProcessInput(ConsoleKey key);
        public void Update();
        public void Render();
    }
}
