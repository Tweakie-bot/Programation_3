using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmation_3
{
    public class OutputRenderer : IOutput
    {
        public void WriteLine(string line_to_write)
        {
            Console.WriteLine(line_to_write);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
