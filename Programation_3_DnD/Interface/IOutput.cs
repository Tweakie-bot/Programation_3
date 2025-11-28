using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Interface
{
    public interface IOutput
    {
        public void WriteLine(string text);
        public void BeginFrame();
        public void EndFrame();
        public void PassLine();
        public void Clear();
    }
}
