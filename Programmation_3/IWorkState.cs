using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public interface IWorkState
    {
        void Enter();

        void Exit();

        void FixedUpdate(float delta_time);
    }
}
