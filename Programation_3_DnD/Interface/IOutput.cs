using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Interface
{
    internal interface IOutput
    {
        public void WriteLine(string text);

        public void PassLine();

        public void Clear();
    }
}
