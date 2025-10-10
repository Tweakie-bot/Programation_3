using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public interface IState
    {
        public void Enter();

        public void Exit();

        public void Input(string input);

        public void Update();

        public void Render();
    }
}
