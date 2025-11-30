using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Programation_3_DnD_Core;

namespace Programation_3_DnD_Console
{
    public class InputProcessor : IInput
    {
        private ConsoleKey _lastKey;
        public InputProcessor() { }

        public void ChangeLastKeyForTests(ConsoleKey key) { _lastKey = key;  }
        public void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                _lastKey = Console.ReadKey(true).Key;
            }
        }
        public void ResetInput() { _lastKey = ConsoleKey.None; }
        public bool CheckKeyAvailable()
        {
            if (Console.KeyAvailable)
            {
                return true;
            }
            return false;
        }
        public int GetNumberPressed()
        {
            int input = 0;

            // D1..D9
            if (_lastKey >= ConsoleKey.D1 && _lastKey <= ConsoleKey.D9)
            {
                input = (_lastKey - ConsoleKey.D0);
            }
            // NumPad1..NumPad9
            else if (_lastKey >= ConsoleKey.NumPad1 && _lastKey <= ConsoleKey.NumPad9)
            {
                input = (_lastKey - ConsoleKey.NumPad0);
            }

            return input;
        }
        public bool IsKeyNone()
        {
            return _lastKey == ConsoleKey.None;
        }
        public bool IsKeyUp()
        {
            return _lastKey == ConsoleKey.UpArrow;
        }

        public bool IsKeyDown()
        {
            return _lastKey == ConsoleKey.DownArrow;
        }

        public bool IsKeyLeft()
        {
            return _lastKey == ConsoleKey.LeftArrow;
        }

        public bool IsKeyRight()
        {
            return _lastKey == ConsoleKey.RightArrow;
        }

        public bool IsKeyValidate()
        {
            return _lastKey == ConsoleKey.Enter;
        }

        public bool IsKeyCancel()
        {
            return _lastKey == ConsoleKey.Escape;
        }

        public bool IsKeyQ()
        {
            return _lastKey == ConsoleKey.Q;
        }

        public bool IsKeyInventory()
        {
            return _lastKey == ConsoleKey.I;
        }

        public bool IsKeyPause()
        {
            return _lastKey == ConsoleKey.P;
        }

        public bool IsKeyTrade()
        {
            return _lastKey == ConsoleKey.T;
        }

        public bool IsKeyWork()
        {
            return _lastKey == ConsoleKey.W;
        }

        public bool IsKeyNumber1()
        {
            return _lastKey == ConsoleKey.D1 || _lastKey == ConsoleKey.NumPad1;
        }

        public bool IsKeyNumber2()
        {
            return _lastKey == ConsoleKey.D2 || _lastKey == ConsoleKey.NumPad2;
        }

        public bool IsKeyNumber3()
        {
            return _lastKey == ConsoleKey.D3 || _lastKey == ConsoleKey.NumPad3;
        }

        public bool IsKeyNumber4()
        {
            return _lastKey == ConsoleKey.D4 || _lastKey == ConsoleKey.NumPad4;
        }

        public bool IsKeyNumber5()
        {
            return _lastKey == ConsoleKey.D5 || _lastKey == ConsoleKey.NumPad5;
        }

        public bool IsKeyNumber6()
        {
            return _lastKey == ConsoleKey.D6 || _lastKey == ConsoleKey.NumPad6;
        }

        public bool IsKeyNumber7()
        {
            return _lastKey == ConsoleKey.D7 || _lastKey == ConsoleKey.NumPad7;
        }

        public bool IsKeyNumber8()
        {
            return _lastKey == ConsoleKey.D8 || _lastKey == ConsoleKey.NumPad8;
        }

        public bool IsKeyNumber9()
        {
            return _lastKey == ConsoleKey.D9 || _lastKey == ConsoleKey.NumPad9;
        }

    }
}
