using System;
using System.Collections.Generic;
using System.Text;

namespace Programation_3_DnD_Core
{
    public interface IInput
    {
        public void ProcessInput();
        public void ResetInput();
        public bool CheckKeyAvailable();
        public int GetNumberPressed();
        public bool IsKeyNone();
        public bool IsKeyUp();
        public bool IsKeyDown();
        public bool IsKeyLeft();
        public bool IsKeyRight();
        public bool IsKeyValidate();
        public bool IsKeyCancel();
        public bool IsKeyQ();
        public bool IsKeyInventory();
        public bool IsKeyPause();
        public bool IsKeyTrade();
        public bool IsKeyWork();
        public bool IsKeyNumber1();
        public bool IsKeyNumber2();
        public bool IsKeyNumber3();
        public bool IsKeyNumber4();
        public bool IsKeyNumber5();
        public bool IsKeyNumber6();
        public bool IsKeyNumber7();
        public bool IsKeyNumber8();
        public bool IsKeyNumber9();
    }
}