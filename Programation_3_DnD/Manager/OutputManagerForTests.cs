using Programation_3_DnD.Interface;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Manager
{
    public class OutputManagerForTests : IOutput
    {
            public void WriteLine(string message) { }

            public void PassLine() { }

            public void Clear() { }

            public void BeginFrame() { }

            public void EndFrame() { }
    }
}
