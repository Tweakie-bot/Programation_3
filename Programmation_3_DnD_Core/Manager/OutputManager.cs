using Programation_3_DnD.Interface;
using Spectre.Console;
using System.Text;

namespace Programation_3_DnD.Manager
{
    public class OutputManager : IOutput
    {
        private StringBuilder _buffer = new StringBuilder();
        private bool _buffering = false;

        public void WriteLine(string message)
        {
            if (_buffering)
            {
                _buffer.AppendLine(message);
            }
            else
            {
                AnsiConsole.WriteLine(message);
            }
        }

        public void PassLine()
        {
            WriteLine(string.Empty);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void BeginFrame()
        {
            _buffer.Clear();
            _buffering = true;
        }

        public void EndFrame()
        {
            _buffering = false;

            AnsiConsole.Cursor.SetPosition(0, 0);

            AnsiConsole.Write(_buffer.ToString());
        }
    }
}
