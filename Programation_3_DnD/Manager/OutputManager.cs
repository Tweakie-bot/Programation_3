using Programation_3_DnD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programation_3_DnD.Manager
{
    internal class OutputManager : IOutput
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void PassLine()
        {
            Console.WriteLine();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
